namespace apps.Engine.Models.Dto
{
    public class EngineParamsDto(EngineParamsDto? data)
    {
        public string? TransId { get; set; } = data?.TransId;
        public string? TransDate { get; set; } = data?.TransDate;
        public string? CompanyCode { get; set; } = data?.CompanyCode;
        public string? SiteCode { get; set; } = data?.SiteCode;
        public string? ZoneCode { get; set; } = data?.ZoneCode;
        public string? EntertainNip { get; set; } = data?.EntertainNip;
        public string? EntertainOtp { get; set; } = data?.EntertainOtp;
        public string? PromoAppCode { get; set; } = data?.PromoAppCode;
        public bool Member { get; set; } = data?.Member ?? false;
        public bool NewMember { get; set; } = data?.NewMember ?? false;
        public string? MemberCode { get; set; } = data?.MemberCode;
        public string? StatusMember { get; set; } = data?.StatusMember;
        public bool PromoVoucher { get; set; } = data?.PromoVoucher ?? false;
        public string? PromoVoucherCode { get; set; } = data?.PromoVoucherCode;
        public List<ItemProduct>? ItemProduct { get; set; } = data?.ItemProduct;
        public List<Mop>? Mop { get; set; } = data?.Mop;
    }

    public class ItemProduct(ItemProduct? data)
    {
        public int LineNo { get; set; } = data?.LineNo ?? 0;
        public string? SkuCode { get; set; } = data?.SkuCode;
        public string? DeptCode { get; set; } = data?.DeptCode;
        public double Qty { get; set; } = data?.Qty ?? 0;
        public decimal Price { get; set; } = data?.Price ?? 0;
        public decimal PriceTemp { get; set; } = data?.PriceTemp ?? 0;
    }

    public class Mop(Mop? data)
    {
        public string? MopCode { get; set; } = data?.MopCode;
        public decimal Amount { get; set; } = data?.Amount ?? 0;
    }
}
