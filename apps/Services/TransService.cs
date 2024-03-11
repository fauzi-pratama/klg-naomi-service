
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
                .Where(q => q.CompanyCode == engineRequest.CompanyCode && q.TransId == engineRequest.TransId && q.ActiveFlag && q.Commited)
                .Include(q => q.Detail)
                .FirstOrDefaultAsync();

            //Return False if Transaction already commited
            if (dataTrans is { Commited: true })
                return (false, $"Transaction Id : {engineRequest.TransId}, is already commited !!");

            return (true, "Success");
        }
    }
}
