
namespace apps.Models.Request
{
    public class EngineRequest()
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
        public List<Mop>? Mop { get; set; }
    }

    public class ItemProduct
    {
        public int LineNo { get; set; }
        public string? SkuCode { get; set; }
        public string? DeptCode { get; set; }
        public double Qty { get; set; }
        public decimal Price { get; set; }
        public decimal PriceTemp { get; set; }

        public ItemProduct() { }

        public ItemProduct(ItemProduct other)
        {
            LineNo = other.LineNo;
            SkuCode = other.SkuCode;
            DeptCode = other.DeptCode;
            Qty = other.Qty;
            Price = other.Price;
            PriceTemp = other.PriceTemp;
        }
    }

    public class Mop
    {
        public string? MopCode { get; set; }
        public decimal Amount { get; set; }

        public Mop() { }

        public Mop(Mop other)
        {
            MopCode = other.MopCode;
            Amount = other.Amount;
        }
    }
}
