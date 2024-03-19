using apps.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Engine.Models.Entities
{
    [Table("promo_master")]
    [Index(nameof(Id))]
    public class PromoMaster : BaseEntities
    {
        [Required]
        [Column("company_code", Order = 1), MaxLength(50)]
        public string? CompanyCode { get; set; }

        [Required]
        [Column("promo_code", Order = 2), MaxLength(50)]
        public string? PromoCode { get; set; }

        [Required]
        [Column("qty", Order = 3)]
        public int? Qty { get; set; }

        [Required]
        [Column("qty_book", Order = 4)]
        public int? QtyBook { get; set; }

        [Required]
        [Column("balance", Order = 5)]
        public int? Balance { get; set; }

        [Required]
        [Column("balance_book", Order = 6)]
        public int? BalanceBook { get; set; }
    }
}
