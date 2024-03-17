using apps.Models.Contexts;
using apps.Models.Request;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace apps.Services
{
    public interface IOtpService
    {
        //Task<bool> ConfirmOtp(EngineRequest engineRequest);
    }

    public class OtpService(DataDbContext dbContext) : IOtpService
    {
        //public async Task<bool> ConfirmOtp(EngineRequest engineRequest)
        //{
        //    return await dbContext.TransOtp.AnyAsync(q => q.CompanyCode!.ToUpper() == engineRequest.CompanyCode!.ToUpper() &&
        //            q.Nip!.ToUpper() == engineRequest.EntertainNip!.ToUpper() && q.UseStatus && q.ExpDate > DateTime.UtcNow);
        //}

        //public async Task<(bool, string)> GetRequestPromoOtp(PromoOtpRequest promoOtpRequest)
        //{
        //    string otpCode;

        //    PromoOtp? promoOtp = _dbProvider.DbContext.PromoOtp.OrderByDescending(q => q.ExpDate)
        //        .FirstOrDefault(q => q.CompanyCode == promoOtpRequest.CompanyCode && q.Nip == promoOtpRequest.Nip && q.ExpDate > DateTime.UtcNow
        //                            && !q.IsUse && q.ActiveFlag);

        //    PromoMasterUserEmail? promoUserEmail = await _dbProvider.DbContext.PromoMasterUserEmail
        //        .FirstOrDefaultAsync(q => q.Nip!.ToUpper() == promoOtpRequest.Nip!.ToUpper() && q.ActiveFlag);

        //    if (promoUserEmail is null || string.IsNullOrEmpty(promoUserEmail.Email))
        //        return (false, $"User {promoOtpRequest.Nip} no Have email on Table PromoMasteruserEmail !!");

        //    string emailName = "";

        //    if (promoUserEmail.Email.Contains('@'))
        //    {
        //        emailName = promoUserEmail.Email.Split('@')[0];

        //        if (string.IsNullOrEmpty(emailName) && emailName.Contains('.'))
        //        {
        //            emailName = emailName.Replace('.', ' ');
        //        }
        //    }

        //    if (promoOtp is not null)
        //    {
        //        promoOtp.ActiveFlag = false;

        //        await _dbProvider.DbContext.SaveChangesAsync();
        //    }

        //    otpCode = GetOtp(6);

        //    if (otpCode == null)
        //        return (false, $"User {promoOtpRequest.Nip} Failed Get Otp");

        //    PromoOtp newPromoOtp = new()
        //    {
        //        Id = Guid.NewGuid(),
        //        CompanyCode = promoOtpRequest.CompanyCode,
        //        Nip = promoOtpRequest.Nip,
        //        Code = otpCode,
        //        ExpDate = DateTime.UtcNow.AddMinutes(5),
        //        IsUse = false,
        //        ActiveFlag = true,
        //        CreatedDate = DateTime.UtcNow
        //    };

        //    _dbProvider.DbContext.PromoOtp.Add(newPromoOtp);

        //    await _dbProvider.DbContext.SaveChangesAsync();

        //    SendEmailMessage sendEmailMessage = new()
        //    {
        //        EmailTemplate = new()
        //        {
        //            EmailType = EmailType.Mjml,
        //            EmailBody = "<mjml>  \r\n  <mj-body>    \r\n    <mj-section>      \r\n      <mj-column>        \r\n        <mj-image width=\"800px\" src=\"https://drive.google.com/uc?id=1MxX0QUAgxdmv0-Ejc7tXnCpbD7NhnuvE\"></mj-image>        \r\n        <mj-divider border-color=\"#0a0a0a\"></mj-divider>        \r\n        <mj-text font-size=\"20px\" color=\"#0a0a0a\" font-family=\"helvetica\">Hello {varName}!</mj-text>                <mj-text font-size=\"20px\" color=\"#0a0a0a\" font-family=\"helvetica\">Berikut kode OTP untuk promo entertaiment : {varOtpCode}, OTP ini berlaku selama 5 menit.</mj-text>                \r\n        <mj-text font-size=\"20px\" color=\"#0a0a0a\" font-family=\"helvetica\">Terima Kasih.</mj-text>      \r\n      </mj-column>    \r\n    </mj-section>  \r\n  </mj-body>\r\n</mjml>",
        //            Parameters = new Dictionary<string, object>
        //            {
        //                {"varOtpCode", otpCode },
        //                {"varName", emailName.ToUpper() }
        //            }
        //        },
        //        Tos = new List<string>
        //        {
        //            promoUserEmail.Email
        //        },

        //        Subject = "OTP Promo"
        //    };

        //    await _messageProvider.PublishAsync(sendEmailMessage);

        //    return (true, "Success");
        //}



        //public async Task SyncEmailUserPromo(PromoEmailUser request)
        //{
        //    PromoMasterUserEmail? promoUserEmail = await _dbProvider.DbContext.PromoMasterUserEmail.
        //        FirstOrDefaultAsync(q => q.Nip!.ToUpper() == request.Nip!.ToUpper());

        //    if (promoUserEmail != null)
        //    {
        //        promoUserEmail.Email = request.Email;
        //        promoUserEmail.ActiveFlag = (bool)request.ActiveFlag!;

        //    }
        //    else
        //    {
        //        promoUserEmail = new()
        //        {
        //            Id = Guid.NewGuid(),
        //            Nip = request.Nip!.ToUpper(),
        //            Email = request.Email,
        //            ActiveFlag = request.ActiveFlag ?? false
        //        };

        //        _dbProvider.DbContext.PromoMasterUserEmail.Add(promoUserEmail);
        //    }

        //    await _dbProvider.DbContext.SaveChangesAsync();
        //}

        //private static string GetOtp(int length)
        //{
        //    Random rdm = new();
        //    string template = "ABCDEFGHIJKLMNOP1RSTUVWXYZ1234567890";
        //    StringBuilder keyRandom = new();

        //    for (int i = 0; i < length; i++)
        //    {
        //        int a = rdm.Next(template.Length);
        //        keyRandom.Append(template.ElementAt(a));
        //    }

        //    return keyRandom.ToString();
        //}
    }
}
