namespace apps.Models.Request
{
    public class FindPromoRequest
    {
        public string? TransId { get; set; }
        public string? TransDate { get; set; }
        public string? CompanyCode { get; set; }
        public string? SiteCode { get; set; }
        public string? ZoneCode { get; set; }
        public string? EntertainNip { get; set; }
        public string? EntertainOtp { get; set; }
        public string? PromoAppCode { get; set; }
        public Boolean Member { get; set; } = false;
        public Boolean NewMember { get; set; } = false;
        public string? MemberCode { get; set; }
        public string? StatusMember { get; set; }
        public Boolean PromoVoucher { get; set; }
        public string? PromoVoucherCode { get; set; }
        public List<ItemProduct>? ItemProduct { get; set; }
    }
}
