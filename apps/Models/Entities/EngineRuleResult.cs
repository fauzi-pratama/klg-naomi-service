
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using apps.Configs;
using Microsoft.EntityFrameworkCore;

namespace apps.Models.Entities
{
    [Table("engine_rule_result")]
    [Index(nameof(EngineRuleCode), nameof(GroupLine), nameof(LineNum))]
    public class EngineRuleResult : BaseEntities
    {
        [ForeignKey("engine_rule")]
        [Column("engine_rule_code", Order = 0), MaxLength(50)]
        public string? EngineRuleCode { get; set; }

        [Key]
        [Required]
        [Column("group_line", Order = 1)]
        public int GroupLine { get; set; }

        [Key]
        [Required]
        [Column("line_num", Order = 2)]
        public int LineNum { get; set; }

        [Required]
        [Column("item", Order = 3), MaxLength(50)]
        public string? Item { get; set; }

        [Required]
        [Column("value", Order = 4)]
        public string? Value { get; set; }

        [Required]
        [Column("max_value", Order = 5)]
        public string? MaxValue { get; set; }

        [Column("link", Order = 6)]
        public string? Link { get; set; }
    }
}
