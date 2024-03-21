namespace apps.Engine.Models.Dto
{
    public class EngineResultDto(EngineResultDto? data)
    {
        public string? TransId { get; set; } = data?.TransId;
        public string? CompanyCode { get; set; } = data?.CompanyCode;
        public string? PromoCode { get; set; } = data?.PromoCode;
        public string? PromoName { get; set; } = data?.PromoName;
        public string? PromoVoucherCode { get; set; } = data?.PromoVoucherCode;
        public string? PromoType { get; set; } = data?.PromoType;
        public string? PromoTypeResult { get; set; } = data?.PromoTypeResult;
        public string? ValDiscount { get; set; } = data?.ValDiscount;
        public string? ValMaxDiscount { get; set; } = data?.ValMaxDiscount;
        public bool ValMaxDiscountStatus { get; set; } = data?.ValMaxDiscountStatus ?? false;
        public int? PromoCls { get; set; } = data?.PromoCls;
        public int? PromoLvl { get; set; } = data?.PromoLvl;
        public int? MaxMultiple { get; set; } = data?.MaxMultiple;
        public int? MaxUse { get; set; } = data?.MaxUse;
        public decimal? MaxBalance { get; set; } = data?.MaxBalance;
        public int? MultipleQty { get; set; } = data?.MultipleQty;
        public string? PromoDesc { get; set; } = data?.PromoDesc;
        public string? PromoTermCondition { get; set; } = data?.PromoTermCondition;
        public string? PromoImageLink { get; set; } = data?.PromoImageLink;
        public bool AbsoluteCombine { get; set; } = data?.AbsoluteCombine ?? false;
        public PromoMopRequire? PromoMopRequire { get; set; } = data?.PromoMopRequire;
        public List<PromoListItem>? PromoListItem { get; set; } = data?.PromoListItem;
    }

    public class PromoMopRequire(PromoMopRequire? data)
    {
        public string? MopPromoSelectionCode { get; set; } = data?.MopPromoSelectionCode;
        public string? MopPromoSelectionName { get; set; } = data?.MopPromoSelectionName;
        public List<PromoMopRequireDetail>? PromoMopRequireDetail { get; set; } = data?.PromoMopRequireDetail;
    }

    public class PromoMopRequireDetail(PromoMopRequireDetail? data)
    {
        public string? MopGroupCode { get; set; } = data?.MopGroupCode;
        public string? MopGroupName { get; set; } = data?.MopGroupName;
    }

    public class PromoListItem(PromoListItem? data)
    {
        public int LinePromo { get; set; } = data?.LinePromo ?? 0;
        public decimal TotalBefore { get; set; } = data?.TotalBefore ?? 0;
        public decimal TotalDiscount { get; set; } = data?.TotalDiscount ?? 0;
        public decimal TotalAfter { get; set; } = data?.TotalAfter ?? 0;
        public int Rounding { get; set; } = data?.Rounding ?? 0;
        public List<PromoListItemDetail>? PromoListItemDetail { get; set; } = data?.PromoListItemDetail;
    }

    public class PromoListItemDetail(PromoListItemDetail? data)
    {
        public int LineNo { get; set; } = data?.LineNo ?? 0;
        public string? SkuCode { get; set; } = data?.SkuCode;
        public string? ValDiscount { get; set; } = data?.ValDiscount;
        public string? ValMaxDiscount { get; set; } = data?.ValMaxDiscount;
        public decimal Price { get; set; } = data?.Price ?? 0;
        public double Qty { get; set; } = data?.Qty ?? 0;
        public decimal TotalPrice { get; set; } = data?.TotalPrice ?? 0;
        public decimal TotalDiscount { get; set; } = data?.TotalDiscount ?? 0;
        public decimal TotalAfter { get; set; } = data?.TotalAfter ?? 0;
    }

    public class ItemGroupResultPerPromo(ItemGroupResultPerPromo? data)
    {
        public string? SkuCode { get; set; } = data?.SkuCode;
        public string? Value { get; set; } = data?.Value;
        public string? MaxValue { get; set; } = data?.MaxValue;
    }
}
