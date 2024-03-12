
using System.Linq.Dynamic.Core;
using apps.Models.Contexts;
using apps.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace apps.Services
{
    public interface ITransService
    {
        Task<(bool status, string message)> CekTransactionExist(EngineRequest engineRequest);
    }

    public class TransService(DataDbContext dbContext) : ITransService
    {
        public async Task<(bool status, string message)> CekTransactionExist(EngineRequest engineRequest)
        {
            //Get Data Transaction Header & Transaction Detail
            var dataTrans = 
                await dbContext.PromoTrans
                .Where(q => q.CompanyCode == engineRequest.CompanyCode && q.TransId == engineRequest.TransId && q.ActiveFlag)
                .Include(q => q.Detail)
                .FirstOrDefaultAsync();

            //Return True, Transaction not exist
            if (dataTrans is null)
                return (true, "Success");

            //Return False if Transaction already commited
            if (dataTrans is { Commited: true })
                return (false, $"Transaction Id : {engineRequest.TransId}, is already commited !!");

            //Rollback Transaction
            Parallel.ForEach(dataTrans.Detail, loopDetailCode =>
            {
                var dataEngineRule = 
                     dbContext.EngineRule
                    .Where(q => q.Code == loopDetailCode.PromoCode && (q.MaxUse > 0 || q.MaxBalance > 0) && q.ActiveFlag)
                    .FirstOrDefault();

                if(dataEngineRule is not null)
                {

                }
            });

            dbContext.PromoTransDetail.RemoveRange(dataTrans.Detail);
            dbContext.PromoTrans.Remove(dataTrans);

            await dbContext.SaveChangesAsync();

            return (true, "Success");
        }
    }
}
