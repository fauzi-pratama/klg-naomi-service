using apps.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Engine.Models.Entities
{
    [Table("promo_transaction")]
    [Index(nameof(Id))]
    public class PromoTransaction : BaseEntities
    {
        [Column("trans_id", Order = 1)]
        public string? TransId { get; set; }

        [Column("company_code", Order = 2), MaxLength(50)]
        public string? CompanyCode { get; set; }

        [Column("total_qty", Order = 3)]
        public int? TotalQty { get; set; }

        [Column("total_balance", Order = 4)]
        public decimal? TotalBalance { get; set; }

        [Column("commited_flag", Order = 5)]
        public bool CommitedFlag { get; set; } = false;

        [Column("check_flag", Order = 5)]
        public bool CheckFlag { get; set; } = false;

        public List<PromoTransactionDetail>? Detail { get; set; }
    }
}
