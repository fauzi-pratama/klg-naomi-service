using apps.Models.Contexts;

namespace apps.Engine.Services
{
    public interface IOtpService
    {
    }

    public class OtpService(DataDbContext dbContext) : IOtpService
    {
        
    }
}
