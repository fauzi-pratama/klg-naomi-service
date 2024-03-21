﻿using apps.Engine.Models.Dto;

namespace apps.Engine.Models.Response
{
    public class FindPromoResponse
    {
        public string? TransId { get; set; }
        public string? CompanyCode { get; set; }
        public string? PromoCode { get; set; }
        public string? PromoName { get; set; }
        public string? PromoVoucherCode { get; set; }
        public string? PromoType { get; set; }
        public string? PromoTypeResult { get; set; }
        public string? ValDiscount { get; set; }
        public string? ValMaxDiscount { get; set; }
        public bool ValMaxDiscountStatus { get; set; } = false;
        public int PromoCls { get; set; } = 0;
        public int PromoLvl { get; set; } = 0;
        public int MaxMultiple { get; set; } = 0;
        public int MaxUse { get; set; } = 0;
        public decimal MaxBalance { get; set; } = 0;
        public int MultipleQty { get; set; } = 0;
        public string? PromoDesc { get; set; }
        public string? PromoTermCondition { get; set; }
        public string? PromoImageLink { get; set; }
        public bool AbsoluteCombine { get; set; } = false;
        public PromoMopRequire? PromoMopRequire { get; set; }
        public List<PromoListItem>? PromoListItem { get; set; }
    }
}
