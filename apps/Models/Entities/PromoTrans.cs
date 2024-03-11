
using apps.Configs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("promo_trans")]
    public class PromoTrans : BaseEntities
    {
        [Key]
        [Column("id", Order = 0)]
        public Guid Id { get; set; }

        [Column("trans_id", Order = 1)]
        public string? TransId { get; set; }

        [Column("company_code", Order = 2), MaxLength(50)]
        public string? CompanyCode { get; set; }

        [Column("total_qty", Order = 3)]
        public int? TotalQty { get; set; }

        [Column("total_balance", Order = 4)]
        public decimal? TotalBalance { get; set; }

        [Column("commited", Order = 5)]
        public bool Commited { get; set; } = false;

        [Column("exp_date", Order = 6)]
        public DateTime ExpDate { get; set; }

        public List<PromoTransDetail>? Detail { get; set; }
    }
}
