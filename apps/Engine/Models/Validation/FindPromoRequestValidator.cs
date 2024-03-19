
using FluentValidation;
using System.Globalization;
using apps.Engine.Models.Request;

namespace apps.Engine.Models.Validation
{
    public class FindPromoRequestValidator : AbstractValidator<FindPromoRequest>
    {
        public FindPromoRequestValidator()
        {
            RuleFor(x => x.TransId)
                .NotNull()
                .NotEmpty()
                .WithMessage("TransId is required");

            RuleFor(x => x.TransDate)
                .NotNull()
                .NotEmpty()
                .Must(ValidDateEngine!)
                .WithMessage("TransDate is required or Failed Format set to 'yyyy-MM-dd HH:mm:ss'");

            RuleFor(x => x.CompanyCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("CompanyCode is required");

            RuleFor(x => x.SiteCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("SiteCode is required");

            RuleFor(x => x.ZoneCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("ZoneCode is required");

            RuleFor(x => x.PromoAppCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("PromoAppCode is required");

            RuleFor(x => x.ItemProduct)
                .NotNull()
                .NotEmpty()
                .Must(q => q!.Count > 0)
                .WithMessage("ItemProduct is required");

            RuleFor(x => x.EntertainNip)
                .Must((x, q) => x.EntertainOtp is not null)
                .WithMessage("EntertainNip is required if EntertainOtp is not null");

            RuleFor(x => x.EntertainOtp)
                .Must((x, q) => x.EntertainNip is not null)
                .WithMessage("EntertainOtp is required if EntertainNip is not null");

            RuleFor(x => x.Member)
                .Must((x, q) => x.MemberCode is not null)
                .Must((x, q) => x.StatusMember is not null)
                .Must((x, q) => x.NewMember)
                .WithMessage("Member is required if MemberCode or StatusMember or NewMember is not null");
        }

        private bool ValidDateEngine(string paramsDate)
        {
            try
            {
                Convert.ToString(DateTime.ParseExact(paramsDate.Replace("/", "-"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
