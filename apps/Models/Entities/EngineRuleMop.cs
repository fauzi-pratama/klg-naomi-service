
using apps.Configs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("engine_rule_mop")]
    [Index(nameof(Id))]
    public class EngineRuleMop : BaseEntities
    {
        [ForeignKey("engine_rule")]
        [Column("engine_rule_id", Order = 1), MaxLength(50)]
        public string? EngineRuleId { get; set; }

        [Required]
        [Column("selection_code", Order = 2), MaxLength(50)]
        public string? SelectionCode { get; set; }

        [Required]
        [Column("selection_name", Order = 3), MaxLength(200)]
        public string? SelectionName { get; set; }

        [Required]
        [Column("group_code", Order = 4), MaxLength(50)]
        public string? GroupCode { get; set; }

        [Required]
        [Column("group_name", Order = 5), MaxLength(200)]
        public string? GroupName { get; set; }

        public List<EngineRuleMopBin>? Bins { get; set; }
    }
}
