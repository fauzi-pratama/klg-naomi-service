
using AutoMapper;
using apps.Models.Contexts;
using apps.Engine.Models.Request;
using apps.Engine.Models.Response;
using apps.Engine.Models.Dto;

namespace apps.Engine.Services
{
    public interface IFindService
    {
        Task<(List<FindPromoResponse> data, bool status, string message)> FindPromo(FindPromoRequest dataRequest);
    }

    public class FindService(DataDbContext dataDbContext, IEngineService engineService, IMapper mapper, IOtpService otpService,
        ITransService transService) : IFindService
    {
        public async Task<(List<FindPromoResponse> data, bool status, string message)> FindPromo(FindPromoRequest dataRequest)
        {
            //Convert to EngineRequest with Mapper
            List<FindPromoResponse> dataResponse = [];
            EngineParamsDto engineRequest = mapper.Map<EngineParamsDto>(dataRequest);

            //Check Company Code
            if (dataDbContext.EngineWorkflows.Any(q => q.Code == engineRequest.CompanyCode && q.ActiveFlag))
                return (dataResponse, false, $"Company Code {engineRequest.CompanyCode} is not registered !!");

            //Cek Validasi Otp Entertain
            //if (!string.IsNullOrWhiteSpace(engineRequest.EntertainNip) && await otpService.ConfirmOtp(engineRequest))
            //    return (dataResponse, false, "Otp is not valid !!");

            //Cek Transaction Already Exist
            var resultCekTransactionExist = await transService.CekTransactionExist(engineRequest);
            if (!resultCekTransactionExist.status)
                return (dataResponse, resultCekTransactionExist.status, resultCekTransactionExist.message);

            #region Cek Transaction Already Exist

            //PromoTrans? cekTransactionCommit = await dataDbContext.PromoTrans
            //    .Include(q => q.PromoTransDetail)
            //    .FirstOrDefaultAsync(q => q.CompanyCode == engineRequest.CompanyCode && q.TransId == engineRequest.TransId && q.ActiveFlag);

            //if (cekTransactionCommit is not null && cekTransactionCommit.Commited)
            //{
            //    return (new List<FindPromoResponse>(), $"TransactionId {engineRequest.TransId} Already Commited", false);
            //}
            //else if (cekTransactionCommit is not null)
            //{
            //    (bool cekRollBack, string messageRollBack) = await _softBookingManager.PromoRollBackBeforeCommit(cekTransactionCommit);

            //    if (!cekRollBack)
            //        return (new List<FindPromoResponse>(), messageRollBack, false);

            //    rollbackStatus = true;
            //}

            #endregion

            #region Bypass Mop

            //List<Mop> listMop = new();
            //engineRequest.SiteCode ??= "";
            //List<PromoMasterMop> listPromoMop = await dataDbContext.PromoMasterMop
            //    .Where(q => q.CompanyCode == engineRequest.CompanyCode && q.SiteCode!.ToUpper() == engineRequest.SiteCode.ToUpper() && q.ActiveFlag).ToListAsync();

            //foreach (var loopPromoMop in listPromoMop)
            //{
            //    Mop mop = new()
            //    {
            //        MopCode = loopPromoMop.MopCode,
            //        Amount = engineRequest.ItemProduct.Sum(q => Convert.ToDecimal(q.Qty) * q.Price)
            //    };

            //    listMop.Add(mop);
            //}

            //engineRequest.Mop = listMop;

            #endregion

            #region Summary Promo Request Params

            //var listItemProductDuplicate = engineRequest.ItemProduct.GroupBy(q => q.SkuCode).Where(q => q.Any()).ToList();

            //List<ItemProduct> listItemProductDuplicateHandle = new();

            //foreach (var loopSkuDuplicate in listItemProductDuplicate)
            //{
            //    var dataItemRequest = engineRequest.ItemProduct.Where(q => q.SkuCode == loopSkuDuplicate.Key).ToList();

            //    if (dataItemRequest is null || !dataItemRequest.Any())
            //        continue;

            //    ItemProduct itemProduct = new()
            //    {
            //        LineNo = dataItemRequest.FirstOrDefault()!.LineNo,
            //        SkuCode = dataItemRequest.FirstOrDefault()!.SkuCode,
            //        DeptCode = dataItemRequest.FirstOrDefault()!.DeptCode,
            //        Qty = dataItemRequest.Sum(q => q.Qty),
            //        Price = dataItemRequest.FirstOrDefault()!.Price,
            //        PriceTemp = dataItemRequest.FirstOrDefault()!.PriceTemp
            //    };

            //    listItemProductDuplicateHandle.Add(itemProduct);

            //    engineRequest.ItemProduct = engineRequest.ItemProduct!.Where(q => q.SkuCode != loopSkuDuplicate.Key).ToList();
            //}

            //engineRequest.ItemProduct.AddRange(listItemProductDuplicateHandle);

            #endregion

            #region Call Engine Promo

            var resultCallEngine = await engineService.GetPromo(engineRequest);

            //Validate Error Call Engine
            if (!resultCallEngine.status)
                return (dataResponse, resultCallEngine.status, $"Failed get promo from engine, {resultCallEngine.message}");

            //Validate Null on Result Engine
            if (resultCallEngine.data is null)
                return (dataResponse, resultCallEngine.status, "No have promo for this cart");

            #endregion

            #region Check Softbooking

            ////Validate Promo Availble Use
            //List<PromoRuleCekAvailRequest> listPromoCekAvail = new(); //variable temp , untuk nampung promo list

            //PromoWorkflow? promoWorkflow = await dataDbContext.PromoWorkflow
            //    .FirstOrDefaultAsync(q => q.Code == engineRequest.CompanyCode && q.ActiveFlag);

            //if (promoWorkflow is null)
            //{
            //    if (rollbackStatus)
            //        await dataDbContext.Database.RollbackTransactionAsync();

            //    return (new List<FindPromoResponse>(), $"Company {engineRequest.CompanyCode} not Registered on Table PromoWorkflow !!", false);
            //}

            //foreach (var loopListPromoResult in listPromoResult)
            //{
            //    PromoRule? cekPromoRef = await dataDbContext.PromoRule.FirstOrDefaultAsync(q => q.Code == loopListPromoResult.PromoCode!
            //        && q.PromoWorkflowId == promoWorkflow!.Id && q.ActiveFlag);

            //    PromoRuleCekAvailRequest promoCekAvail = new()
            //    {
            //        PromoCode = loopListPromoResult.PromoCode,
            //        BalanceUse = loopListPromoResult.PromoListItem!.Count < 1 ? 0 : (decimal)loopListPromoResult.PromoListItem!.FirstOrDefault()!.TotalDiscount!,
            //        QtyUse = cekPromoRef!.MultipleQty == 0 ? 1 : cekPromoRef.MultipleQty,
            //        MaxBalance = (decimal)loopListPromoResult.MaxBalance!,
            //        MaxUse = (int)loopListPromoResult.MaxUse!,
            //        CekRefCode = cekPromoRef!.RefCode != null,
            //        RefCode = cekPromoRef!.RefCode != null ? cekPromoRef.RefCode : null
            //    };

            //    listPromoCekAvail.Add(promoCekAvail);
            //}

            ////Call Function Promo Avail
            //(List<string> listPromoAvail, string message) = await _softBookingManager.CekPromoAvail(listPromoCekAvail, engineRequest.CompanyCode!);

            //if (message != "SUCCESS" || listPromoAvail is null || listPromoAvail.Count < 1)
            //{
            //    if (rollbackStatus)
            //        await dataDbContext.Database.RollbackTransactionAsync();

            //    return (new List<FindPromoResponse>(), $"FAILED CEK PROMO AVAIL, {message}", false);
            //}

            ////Filter Promo Result dengan promo avail
            //listPromoResult = listPromoResult.Where(q => listPromoAvail.Contains(q.PromoCode)).ToList();

            //if (listPromoResult is null || listPromoResult.Count < 1)
            //{
            //    if (rollbackStatus)
            //        await dataDbContext.Database.RollbackTransactionAsync();

            //    return (new List<FindPromoResponse>(), "No Have Promo for This Cart", true);
            //}

            #endregion

            #region Check Promo Entertain

            ////Filter Show Entertain Promo If Enterain Nip is Available
            //if (!string.IsNullOrEmpty(engineRequest.EntertainNip) && !string.IsNullOrEmpty(engineRequest.EntertainOtp))
            //{
            //    listPromoResult = listPromoResult.Where(q => q.PromoCls == 4).ToList();

            //    if (listPromoResult.Count < 1)
            //    {
            //        if (rollbackStatus)
            //            await dataDbContext.Database.RollbackTransactionAsync();

            //        return (new List<FindPromoResponse>(), "No Have Promo Entertaint for This Cart", true);
            //    }

            //    return (listPromoResult, "SUCCESS", true);
            //}

            #endregion

            #region Check Promo Member Per Periode

            ////Filter Member Use Periode
            //if (engineRequest.Member && string.Equals(engineRequest.PromoAppCode, "PRD00002", StringComparison.OrdinalIgnoreCase) && listPromoResult.Count > 0)
            //{
            //    //Get List Promo Code from Promo Result
            //    List<string> listPromo = listPromoResult.Select(q => q.PromoCode!).ToList();

            //    if (listPromo is null || listPromo.Count < 1)
            //    {
            //        if (rollbackStatus)
            //            await dataDbContext.Database.RollbackTransactionAsync();

            //        return (new List<FindPromoResponse>(), "No Have Promo for This Cart at Promo Member Per Periode", true);
            //    }

            //    //Get Data Promo Rule Db
            //    List<PromoRule> listPromoRule = await dataDbContext.PromoRule
            //        .Where(q => q.PromoWorkflowId == promoWorkflow.Id && q.ActiveFlag)
            //        .ToListAsync();

            //    if (listPromoRule is null || listPromoRule.Count < 1)
            //    {
            //        if (rollbackStatus)
            //            await dataDbContext.Database.RollbackTransactionAsync();

            //        return (new List<FindPromoResponse>(),
            //            $"CompanyId {promoWorkflow.Id} is null on Table PromoRule at Promo Member Per Periode", false);
            //    }

            //    //Get Data Promo Trans Detail Db Base on List Promo Code
            //    List<PromoTransDetail> promoTransDetails = await dataDbContext.PromoTransDetail.Where(q => listPromo.Contains(q.PromoCode!)).ToListAsync();

            //    //Get List Promo Trans Id from Promo Trans Detail
            //    List<Guid?> listPromoTransId = promoTransDetails.Select(q => q.PromoTransId).ToList();

            //    //Get Data Promo Trans Base on List Promo Trans Id
            //    List<PromoTrans> promoTrans = await dataDbContext.PromoTrans.Where(q => listPromoTransId.Contains(q.Id)).ToListAsync();

            //    //Looping Promo Result for Check Member Use on Periode
            //    foreach (var looListPromoResult in listPromoResult)
            //    {
            //        //Get Data Promo Rule Base on Promo Code
            //        PromoRule? promoRule = listPromoRule.FirstOrDefault(q => q.Code == looListPromoResult.PromoCode);

            //        if (promoRule is null)
            //            return (new List<FindPromoResponse>(),
            //                $"Promo Code {looListPromoResult.PromoCode} not Registered on Table PromoRule at Promo Member Per Periode!!", false);

            //        //Get Periode Promo
            //        DateTime periodePromo = DateTime.Now.AddDays(-promoRule.MemberRepeatPeriode);

            //        //Get Data Promo Trans Base on Promo Code and Periode Promo
            //        var dataReport = from transDetail in promoTransDetails
            //                            .Where(q => q.PromoCode == looListPromoResult.PromoCode && q.MemberCode == engineRequest.MemberCode)
            //                         from transHeader in promoTrans
            //                            .Where(q => q.Id == transDetail.PromoTransId && q.ActiveFlag && q.TransDate > periodePromo)
            //                         select new
            //                         {
            //                             transHeader.Id,
            //                             transHeader.TransDate,
            //                             transDetail.PromoTotal,
            //                             transDetail.ReasonType,
            //                             transDetail.Remarks,
            //                             transDetail.QtyPromo
            //                         };

            //        //Filtering Promo Result , if promo exist on periode
            //        if (dataReport is not null && dataReport.Any() && dataReport.Sum(q => q.QtyPromo) >= promoRule.MemberQuotaPeriode &&
            //            promoRule.MemberQuotaPeriode != 0 && promoRule.MemberQuotaPeriode != 0)
            //            listPromoResult = listPromoResult.Where(q => q.PromoCode != looListPromoResult.PromoCode).ToList();
            //    }

            //    if (listPromoResult.Count < 1)
            //    {
            //        if (rollbackStatus)
            //            await dataDbContext.Database.RollbackTransactionAsync();

            //        return (new List<FindPromoResponse>(), "No Have Promo for This Cart at Promo Member Per Periode", true);
            //    }
            //}

            #endregion

            return (dataResponse, true, "Success");
        }
    }
}
