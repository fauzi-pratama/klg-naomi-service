
using apps.Configs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("promo_trans_detail")]
    [Index(nameof(PromoTransId), nameof(PromoCode))]
    public class PromoTransDetail : BaseEntities
    {
        [ForeignKey("promo_trans")]
        [Column("promo_trans_id", Order = 0), MaxLength(50)]
        public Guid? PromoTransId { get; set; }

        [Key]
        [Column("promo_code", Order = 1), MaxLength(50)]
        public string? PromoCode { get; set; }

        [Column("otp_code", Order = 2), MaxLength(50)]
        public string? OtpCode { get; set; }

        [Column("zone_code", Order = 3), MaxLength(50)]
        public string? ZoneCode { get; set; }

        [Column("site_code ", Order = 4), MaxLength(50)]
        public string? SiteCode { get; set; }

        [Column("member_status", Order = 5)]
        public bool MemberStatus { get; set; } = false;

        [Column("member_new_status", Order = 6)]
        public bool MemberNewStatus { get; set; } = false;

        [Column("member_code", Order = 7), MaxLength(50)]
        public string? MemberCode { get; set; }

        [Column("member_status_code", Order = 8), MaxLength(50)]
        public string? MemberStatusCode { get; set; }

        [Column("apps_code", Order = 9), MaxLength(50)]
        public string? AppsCode { get; set; }

        [Column("qty", Order = 10)]
        public int? Qty { get; set; }

        [Column("balance", Order = 11)]
        public decimal? Balance { get; set; }

        [Column("reason_type", Order = 12), MaxLength(200)]
        public string? ReasonType { get; set; }

        [Column("remarks", Order = 13), DataType("text")]
        public string? Remarks { get; set; }
    }
}
