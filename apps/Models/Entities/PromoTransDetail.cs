
using apps.Configs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("promo_trans_detail")]
    public class PromoTransDetail : BaseEntities
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [ForeignKey("promo_trans")]
        [Column("promo_trans_id", Order = 1), MaxLength(50)]
        public Guid? PromoTransId { get; set; }

        [Column("promo_code", Order = 2), MaxLength(50)]
        public string? PromoCode { get; set; }

        [Column("otp_code", Order = 3), MaxLength(50)]
        public string? OtpCode { get; set; }

        [Column("zone_code", Order = 4), MaxLength(50)]
        public string? ZoneCode { get; set; }

        [Column("site_code ", Order = 5), MaxLength(50)]
        public string? SiteCode { get; set; }

        [Column("member_status", Order = 6)]
        public bool MemberStatus { get; set; } = false;

        [Column("member_new_status", Order = 7)]
        public bool MemberNewStatus { get; set; } = false;

        [Column("member_code", Order = 8), MaxLength(50)]
        public string? MemberCode { get; set; }

        [Column("member_status_code", Order = 9), MaxLength(50)]
        public string? MemberStatusCode { get; set; }

        [Column("apps_code", Order = 10), MaxLength(50)]
        public string? AppsCode { get; set; }

        [Column("qty", Order = 11)]
        public int? Qty { get; set; }

        [Column("balance", Order = 12)]
        public decimal? Balance { get; set; }

        [Column("reason_type", Order = 13), MaxLength(200)]
        public string? ReasonType { get; set; }

        [Column("remarks", Order = 14), DataType("text")]
        public string? Remarks { get; set; }
    }
}
