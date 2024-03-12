
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using apps.Configs;
using Microsoft.EntityFrameworkCore;

namespace apps.Models.Entities
{
    [Table("engine_rule_mop")]
    [Index(nameof(EngineRuleCode), nameof(SelectionCode), nameof(GroupCode))]
    public class EngineRuleMop : BaseEntities
    {
        [ForeignKey("engine_rule")]
        [Column("engine_rule_code", Order = 0), MaxLength(50)]
        public string? EngineRuleCode { get; set; }

        [Key]
        [Required]
        [Column("selection_code", Order = 1), MaxLength(50)]
        public string? SelectionCode { get; set; }

        [Required]
        [Column("selection_name", Order = 2), MaxLength(200)]
        public string? SelectionName { get; set; }

        [Key]
        [Required]
        [Column("group_code", Order = 3), MaxLength(50)]
        public string? GroupCode { get; set; }

        [Required]
        [Column("group_name", Order = 4), MaxLength(200)]
        public string? GroupName { get; set; }
    }
}
