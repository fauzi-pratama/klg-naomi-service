
using Newtonsoft.Json;
using RulesEngine.Models;
using RulesEngine.Actions;
using apps.Engine.Models.Entities;
using apps.Engine.Models.Dto;

namespace apps.Engine.Helpers
{
    public class EngineResult : ActionBase
    {
        public override async ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters)
        {
            //Get Data Cart
            EngineParamsDto? dataCart = JsonConvert.DeserializeObject<EngineParamsDto>(
                                        JsonConvert.SerializeObject(
                                            ruleParameters.Where(q => q.Name == "paramsPromo").Select(q => q.Value).FirstOrDefault()
                                            )
                                        );

            //Get Data Promo Result
            var dataPromo = JsonConvert.DeserializeObject<EngineRule>(context.GetContext<string>("datapromo"));

            //Return Calculate per Promo
            return await CalculatePromoSingle(dataPromo!, dataCart!);
        }

        public static async Task<EngineResultDto> CalculatePromoSingle(EngineRule dataPromo, EngineParamsDto dataCart)
        {
            int linePromoCount = 1;
            List<PromoListItem> responseDetail = []; //Variable untuk save data detail item semua group

            //Execute Promo Untuk Semua Item
            if (dataPromo.ItemType == "ALL")
            {
                List<PromoListItemDetail> responseDetailGroup = [];

                // Looping Item di Cart untuk Execute Promo
                foreach (var loopItemCart in dataCart.ItemProduct!)
                {

                    decimal discountTypeAll = 0;

                    if (dataPromo.ActionType == "AMOUNT")
                    {
                        //Execute Promo Amount

                        if (dataPromo.Cls > 1)
                        {
                            //Execute Promo Cart & Additional

                            //Get Total Harga Per SKU
                            decimal totalPriceSku = Convert.ToDecimal(loopItemCart.Qty) * loopItemCart.Price;

                            //Get Total Harga All Cart
                            decimal totalPriceCart = dataCart.ItemProduct.Sum(q => q.Price * Convert.ToDecimal(q.Qty));

                            //Get Discount Prorate di Cart
                            discountTypeAll = Math.Floor(totalPriceSku / totalPriceCart * Convert.ToDecimal(dataPromo.ActionValue));
                        }
                        else
                        {
                            //Execute Promo Item
                            discountTypeAll = Convert.ToDecimal(dataPromo.ActionValue);
                        }
                    }

                    if (dataPromo.ActionType == "PERCENT")
                    {
                        //Execute Promo Percent

                        discountTypeAll = Convert.ToDecimal(loopItemCart.Qty) * loopItemCart.Price * Convert.ToDecimal(dataPromo.ActionValue!.Replace("%", "")) / 100;
                    }

                    PromoListItemDetail ResponseDetailGroupSingle = new(null)
                    {
                        LineNo = loopItemCart.LineNo,
                        SkuCode = loopItemCart.SkuCode,
                        ValDiscount = dataPromo.ActionValue,
                        ValMaxDiscount = dataPromo.MaxValue,
                        Price = loopItemCart.Price,
                        Qty = loopItemCart.Qty,
                        TotalPrice = loopItemCart.Price * Convert.ToDecimal(loopItemCart.Qty),
                        TotalDiscount = discountTypeAll,
                        TotalAfter = loopItemCart.Price * Convert.ToDecimal(loopItemCart.Qty) - discountTypeAll
                    };

                    responseDetailGroup.Add(ResponseDetailGroupSingle);
                }

                //Declare for Result Detail Promo
                int roundingAllItem = dataPromo.ActionType == "AMOUNT" && dataPromo.Cls > 1 ?
                                Convert.ToInt32(dataPromo.ActionValue) - Convert.ToInt32(responseDetailGroup.Sum(q => q.TotalDiscount))
                                : 0;
                decimal? total_beforeAllItem = dataCart.ItemProduct.Sum(q => Convert.ToDecimal(q.Qty) * q.Price);
                decimal? total_discountAllItem = responseDetailGroup.Sum(q => q.TotalDiscount) + Convert.ToDecimal(roundingAllItem);
                decimal? total_afterAllItem = total_beforeAllItem - total_discountAllItem;

                PromoListItem responseDetailSingle = new(null)
                {
                    LinePromo = linePromoCount,
                    Rounding = roundingAllItem,
                    TotalBefore = (decimal)total_beforeAllItem,
                    TotalDiscount = (decimal)total_discountAllItem,
                    TotalAfter = (decimal)total_afterAllItem,
                    PromoListItemDetail = responseDetailGroup
                };

                responseDetail.Add(responseDetailSingle);
            }
            //Execute Promo Untuk Custom Item
            else if (dataPromo.ItemType == "CUSTOM")
            {
                //Grouping item bedasarkan group di db ms_promo_rule_result
                List<List<ItemGroupResultPerPromo>> listItemPerPromo = []; //Variable untuk menampung item custom group
                var groupList = dataPromo.Results!.Select(q => q.GroupLine).Distinct().ToList(); //get data disctinc group

                //Get Data Qty Total Bundle
                double totalQtyItemBundle = 0;
                decimal totalPriceItemBudle = 0;
                if (dataPromo.ActionType == "BUNDLE")
                {
                    List<string> itemGetDiscountBundle = [];

                    foreach (var loopItem in dataPromo.Results!)
                    {
                        itemGetDiscountBundle.Add(loopItem.Item!);
                        totalPriceItemBudle += dataCart.ItemProduct!.Find(q => q.SkuCode == loopItem.Item)!.Price *
                            Convert.ToDecimal(loopItem.Value);
                    }

                    totalQtyItemBundle = dataPromo.Results!.Sum(q => Convert.ToDouble(q.Value));
                }

                //Looping group untuk mendapatkan item
                foreach (var loopGroup in groupList)
                {
                    List<ItemGroupResultPerPromo> listItemPerGroup = []; //Varibel untuk menampung item di 1 group
                    var listItemGroup = dataPromo.Results!.Where(q => q.GroupLine == loopGroup).ToList(); //get data item sesuai looping group

                    foreach (var loopItemGroup in listItemGroup)
                    {
                        //Input data item ke list dalam 1 group
                        ItemGroupResultPerPromo ItemPerPromo = new(null)
                        {
                            SkuCode = loopItemGroup.Item,
                            Value = loopItemGroup.Value,
                            MaxValue = loopItemGroup.MaxValue
                        };

                        listItemPerGroup.Add(ItemPerPromo);
                    }

                    listItemPerPromo.Add(listItemPerGroup);
                }

                //Looping item per group
                foreach (var loopItemperPromo in listItemPerPromo)
                {

                    List<PromoListItemDetail> responseDetailGroup = []; //Model for save hasil promo item per group

                    decimal discountTypeCustom = 0;
                    List<string> dataListItem = [];

                    foreach (var loopGetDataItemPerPromo in loopItemperPromo)
                    {
                        dataListItem.Add(loopGetDataItemPerPromo.SkuCode!);
                    }

                    /* Start Untuk Cek Item AND Jika tidak ada salah satu maka tidak dapat promo */
                    List<string> dataItemGroup = [];

                    foreach (var loopItemGroupString in loopItemperPromo)
                    {
                        dataItemGroup.Add(loopItemGroupString.SkuCode!);
                    }

                    //Untuk Cek Custom Item Dan Harus Terdapat Semua Item
                    bool executePromoCekResultType = false;
                    var cekItemGroupInCart = dataCart.ItemProduct!.Where(q => dataItemGroup.Contains(q.SkuCode!));

                    executePromoCekResultType = cekItemGroupInCart.Count() == loopItemperPromo.Count || dataPromo.ResultType == "V2";

                    if (executePromoCekResultType)
                    {
                        foreach (var loopItemPerGroup in loopItemperPromo)
                        {
                            PromoListItemDetail responseDetailGroupSingle = new(null);
                            var dataItemCart = dataCart.ItemProduct!.Find(q => q.SkuCode == loopItemPerGroup.SkuCode); //Get data di cart

                            if (dataItemCart != null)
                            {
                                int qtyMax = dataPromo.MaxQty > dataItemCart.Qty ? Convert.ToInt32(dataItemCart.Qty) : dataPromo.MaxQty;

                                if (dataPromo.ActionType == "AMOUNT")
                                { //Execute Promo Amount
                                    if (dataPromo.Cls > 1)
                                    {
                                        //Execute Untuk Class Selain Item
                                        //Get Total Harga Per SKU
                                        decimal totalPriceSku = Convert.ToDecimal(dataItemCart.Qty) * dataItemCart.Price;

                                        //Get Total Harga Custom Item
                                        decimal totalPriceCart = dataCart.ItemProduct!.Where(q => dataListItem.Contains(q.SkuCode!))
                                            .Sum(q => q.Price * Convert.ToDecimal(q.Qty));

                                        //Get Discount Prorate di Cart Untuk Custom Item
                                        discountTypeCustom = Math.Floor(totalPriceSku / totalPriceCart * Convert.ToDecimal(dataPromo.ActionValue));
                                    }
                                    else
                                    {
                                        //Execute Untuk Class Item
                                        discountTypeCustom = Convert.ToDecimal(loopItemPerGroup.Value) * qtyMax;
                                    }
                                }
                                else if (dataPromo.ActionType == "ITEMVALUE")
                                {
                                    if (dataPromo.Cls == 1)
                                    {
                                        if (Convert.ToDecimal(loopItemPerGroup.Value) > Convert.ToDecimal(dataItemCart.Price))
                                            discountTypeCustom = 0;
                                        else
                                            discountTypeCustom = (Convert.ToDecimal(dataItemCart.Price) - Convert.ToDecimal(loopItemPerGroup.Value)) * qtyMax;
                                    }
                                }
                                else if (dataPromo.ActionType == "PERCENT")
                                { //Execute Promo Percent

                                    if (dataPromo.Cls == 1)
                                    {
                                        discountTypeCustom = dataItemCart.Price * Convert.ToDecimal(qtyMax) *
                                        Convert.ToDecimal(loopItemPerGroup.Value!.Replace("%", "")) / 100;
                                    }
                                    else
                                    {
                                        discountTypeCustom = dataItemCart.Price * Convert.ToDecimal(dataItemCart.Qty) *
                                        Convert.ToDecimal(loopItemPerGroup.Value!.Replace("%", "")) / 100;
                                    }
                                }
                                else if (dataPromo.ActionType == "ITEM")
                                {
                                    //Execute Promo Item
                                    if (dataPromo.Cls == 1)
                                        discountTypeCustom = dataItemCart.Price * Convert.ToDecimal(loopItemPerGroup.Value);
                                    else
                                        discountTypeCustom = dataItemCart.Price * dataPromo.MultipleQty;
                                }
                                else if (dataPromo.ActionType == "SP")
                                {
                                    //Execute Promo Special Price
                                    if (dataPromo.Cls == 1)
                                        discountTypeCustom = (dataItemCart.Price - Convert.ToDecimal(loopItemPerGroup.Value)) *
                                            Convert.ToDecimal(qtyMax);
                                    else
                                        discountTypeCustom = (dataItemCart.Price - Convert.ToDecimal(loopItemPerGroup.Value)) *
                                            Convert.ToDecimal(dataItemCart.Qty);
                                }
                                else if (dataPromo.ActionType == "BUNDLE")
                                {
                                    discountTypeCustom = Math.Floor(Convert.ToDecimal(loopItemPerGroup.Value) /
                                        (decimal)totalQtyItemBundle * (Convert.ToDecimal(totalPriceItemBudle) - Convert.ToDecimal(dataPromo.ActionValue)));
                                }
                                else
                                {
                                    discountTypeCustom = 0;
                                }

                                responseDetailGroupSingle.LineNo = dataItemCart.LineNo;
                                responseDetailGroupSingle.SkuCode = dataItemCart.SkuCode;
                                responseDetailGroupSingle.ValDiscount = loopItemPerGroup.Value;
                                responseDetailGroupSingle.ValMaxDiscount = loopItemPerGroup.MaxValue;
                                responseDetailGroupSingle.Price = dataItemCart.Price;
                                responseDetailGroupSingle.Qty = dataItemCart.Qty;
                                responseDetailGroupSingle.TotalPrice = dataItemCart.Price * Convert.ToDecimal(dataItemCart.Qty);
                                responseDetailGroupSingle.TotalDiscount = discountTypeCustom;
                                responseDetailGroupSingle.TotalAfter = dataItemCart.Price * Convert.ToDecimal(dataItemCart.Qty) - discountTypeCustom;

                                responseDetailGroup.Add(responseDetailGroupSingle);
                            }
                        }
                    }

                    if (responseDetailGroup.Count > 0)
                    {
                        //Declare for Result Detail Promo

                        int roundingAllItem = 0;
                        if (dataPromo.ActionType == "AMOUNT" && dataPromo.Cls > 1)
                        {
                            roundingAllItem = Convert.ToInt32(dataPromo.ActionValue) - Convert.ToInt32(responseDetailGroup.Sum(q => q.TotalDiscount));
                        }
                        else if (dataPromo.ActionType == "BUNDLE")
                        {
                            roundingAllItem = Convert.ToInt32(totalPriceItemBudle) - Convert.ToInt32(dataPromo.ActionValue) - Convert.ToInt32(responseDetailGroup.Sum(q => q.TotalDiscount));
                        }

                        decimal? total_beforeAllItem = dataCart.ItemProduct!.Sum(q => Convert.ToDecimal(q.Qty) * q.Price);
                        decimal? total_discountAllItem = responseDetailGroup.Sum(q => q.TotalDiscount) + Convert.ToDecimal(roundingAllItem);
                        decimal? total_afterAllItem = total_beforeAllItem - total_discountAllItem;

                        PromoListItem responseDetailSingle = new(null)
                        {
                            LinePromo = linePromoCount,
                            Rounding = roundingAllItem,
                            TotalBefore = (decimal)total_beforeAllItem,
                            TotalDiscount = (decimal)total_discountAllItem,
                            TotalAfter = (decimal)total_afterAllItem,
                            PromoListItemDetail = [.. responseDetailGroup.OrderBy(q => q.LineNo)]
                        };

                        responseDetail.Add(responseDetailSingle);

                        linePromoCount += 1;
                    }
                }
            }

            //Varibale Save data to Response
            EngineResultDto response = new(null)
            {
                TransId = dataCart.TransId,
                CompanyCode = dataCart.CompanyCode,
                PromoCode = dataPromo.Code,
                PromoName = dataPromo.Name,
                PromoVoucherCode = dataPromo.VoucherCode,
                PromoType = dataPromo.ActionType,
                PromoTypeResult = dataPromo.ItemType,
                ValDiscount = dataPromo.ActionValue,
                ValMaxDiscount = dataPromo.MaxValue,
                PromoCls = dataPromo.Cls,
                PromoLvl = dataPromo.Lvl,
                MaxMultiple = dataPromo.MaxMultiple,
                MaxUse = dataPromo.MaxUse,
                MaxBalance = dataPromo.MaxBalance,
                MultipleQty = dataPromo.MultipleQty,
                PromoDesc = dataPromo.Description,
                PromoTermCondition = dataPromo.TermsCondition,
                PromoImageLink = dataPromo.ImageLink,
                AbsoluteCombine = dataPromo.AbsoluteFlag,
                PromoListItem = [.. responseDetail.OrderByDescending(q => q.TotalDiscount)]
            };

            //Save Data Require MOP
            if (dataPromo.Cls == 3 && dataPromo.Mops != null && dataPromo.Mops.Count > 0)
            {
                List<PromoMopRequireDetail> listPromoMopRequireDetail = [];

                foreach (var loopReqMop in dataPromo.Mops!)
                {
                    PromoMopRequireDetail promoMopRequireDetail = new(null)
                    {
                        MopGroupCode = loopReqMop.GroupCode,
                        MopGroupName = loopReqMop.GroupName
                    };

                    listPromoMopRequireDetail.Add(promoMopRequireDetail);
                }

                PromoMopRequire? promoMopRequire = new(null)
                {
                    MopPromoSelectionCode = dataPromo.Mops!.FirstOrDefault()!.SelectionCode,
                    MopPromoSelectionName = dataPromo.Mops!.FirstOrDefault()!.SelectionName,
                    PromoMopRequireDetail = listPromoMopRequireDetail
                };

                response.PromoMopRequire = promoMopRequire;
            }

            //Execute Maximal Item Jika Promo Percentage
            if (response.PromoType == "PERCENT")
            {
                bool cekMaxPromoStatus = false;

                //Class Item
                if (response.PromoCls == 1)
                {
                    foreach (var loopPromoList in response.PromoListItem)
                    {
                        foreach (var loopPromoListDetail in loopPromoList.PromoListItemDetail!)
                        {
                            if (loopPromoListDetail.ValMaxDiscount != null && loopPromoListDetail.ValMaxDiscount != "" && loopPromoListDetail.ValMaxDiscount != "0"
                                && loopPromoListDetail.TotalDiscount > Convert.ToDecimal(loopPromoListDetail.ValMaxDiscount))
                            {
                                loopPromoListDetail.TotalDiscount = Convert.ToDecimal(loopPromoListDetail.ValMaxDiscount);
                                loopPromoListDetail.TotalAfter = loopPromoListDetail.TotalPrice - loopPromoListDetail.TotalDiscount;
                                cekMaxPromoStatus = true;
                            }
                        }

                        loopPromoList.TotalDiscount = loopPromoList.PromoListItemDetail.Sum(q => q.TotalDiscount);
                        loopPromoList.TotalAfter = loopPromoList.TotalBefore - loopPromoList.TotalDiscount;
                    }

                    response.ValMaxDiscountStatus = cekMaxPromoStatus;
                }
                //Class Cart
                else if ((response.PromoCls == 2 || response.PromoCls == 3) &&
                    response.ValMaxDiscount != null && response.ValMaxDiscount != "")
                {
                    //Bongkar List dan Cari Total Discount yang lebih besar dari Max Discount
                    foreach (var loopDetailPromo in response.PromoListItem)
                    {
                        //Cek Total Disctount melebihi Max Discount
                        if (loopDetailPromo.TotalDiscount > Convert.ToDecimal(response.ValMaxDiscount))
                        {
                            foreach (var loopDetailPromoItem in loopDetailPromo.PromoListItemDetail!)
                            {
                                //Execute Promo Cart & Additional

                                //Get Total Harga Per SKU
                                decimal? totalPriceSku = Convert.ToDecimal(loopDetailPromoItem.Qty) * loopDetailPromoItem.Price;

                                //Get Total Harga All Cart
                                decimal? totalPriceCart = loopDetailPromo.PromoListItemDetail.Sum(q => q.Price * Convert.ToDecimal(q.Qty));

                                //Get Discount Prorate di Cart
                                decimal? totalDiscountCovert = totalPriceSku / totalPriceCart * Convert.ToDecimal(response.ValMaxDiscount);

                                loopDetailPromoItem.TotalDiscount = Math.Floor(totalDiscountCovert ?? 0);
                                loopDetailPromoItem.TotalAfter = (decimal)totalPriceSku - Math.Floor(totalDiscountCovert ?? 0);
                            }

                            //ReCalculate Convert Amount
                            loopDetailPromo.TotalDiscount = loopDetailPromo.PromoListItemDetail.Sum(q => q.TotalDiscount);
                            loopDetailPromo.TotalAfter = loopDetailPromo.PromoListItemDetail.Sum(q => q.TotalAfter);
                            loopDetailPromo.Rounding = Convert.ToInt32(response.ValMaxDiscount) - (int)loopDetailPromo.TotalDiscount!;
                            loopDetailPromo.TotalDiscount += loopDetailPromo.Rounding;
                            loopDetailPromo.TotalAfter -= loopDetailPromo.Rounding;
                            cekMaxPromoStatus = true;
                        }
                    }

                    response.ValMaxDiscountStatus = cekMaxPromoStatus;
                }
            }

            //Handle Message Asyn
            await Task.Run(() =>
            {
                Task.Delay(1).Wait();
            });

            return response;
        }
    }
}
