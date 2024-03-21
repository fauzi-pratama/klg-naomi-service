
using System.Linq.Dynamic.Core;
using apps.Engine.Models.Dto;
using apps.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace apps.Engine.Services
{
    public interface ITransService
    {
        Task<(bool status, string message)> CekTransactionExist(EngineParamsDto engineRequest);
    }

    public class TransService(DataDbContext dbContext) : ITransService
    {
        public async Task<(bool status, string message)> CekTransactionExist(EngineParamsDto engineRequest)
        {
            //Get Data Transaction Header & Transaction Detail
            var dataTrans =
                await dbContext.PromoTransactions
                .Where(q => q.CompanyCode == engineRequest.CompanyCode && q.TransId == engineRequest.TransId && q.ActiveFlag)
                .Include(q => q.Detail)
                .FirstOrDefaultAsync();

            //Return True, Transaction not exist
            if (dataTrans is null)
                return (true, "Success");

            //Return False if Transaction already commited
            if (dataTrans is { CommitedFlag: true })
                return (false, $"Transaction Id : {engineRequest.TransId}, is already commited !!");

            //Rollback Transaction
            Parallel.ForEach(dataTrans.Detail, loopDetailCode =>
            {
                var dataEngineRule =
                     dbContext.EngineRules
                    .Where(q => q.Code == loopDetailCode.PromoCode && (q.MaxUse > 0 || q.MaxBalance > 0) && q.ActiveFlag)
                    .FirstOrDefault();
            });

            dbContext.PromoTransactionDetails.RemoveRange(dataTrans.Detail);
            dbContext.PromoTransactions.Remove(dataTrans);

            await dbContext.SaveChangesAsync();

            return (true, "Success");
        }
    }
}
