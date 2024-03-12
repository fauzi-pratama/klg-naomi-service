
using apps.Configs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("promo_otp")]
    [Index(nameof(Id))]
    public class PromoOtp : BaseEntities
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [Column("company_code", Order = 1), MaxLength(50)]
        public string? CompanyCode { get; set; }

        [Column("nip", Order = 2), MaxLength(50)]
        public string? Nip { get; set; }

        [Column("code", Order = 3), MaxLength(50)]
        public string? Code { get; set; }

        [Column("use_status", Order = 4)]
        public bool UseStatus { get; set; }

        [Column("exp_date", Order = 5)]
        public DateTime ExpDate { get; set; }
    }
}
