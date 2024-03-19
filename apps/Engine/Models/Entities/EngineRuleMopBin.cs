using apps.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Engine.Models.Entities
{
    [Table("engine_rule_mop_bin")]
    [Index(nameof(Id))]
    public class EngineRuleMopBin : BaseEntities
    {
        [ForeignKey("engine_rule_mop")]
        [Column("engine_rule_mop_id", Order = 1), MaxLength(50)]
        public string? EngineRuleMopId { get; set; }

        [Required]
        [Column("code", Order = 2), MaxLength(50)]
        public string? Code { get; set; }
    }
}
