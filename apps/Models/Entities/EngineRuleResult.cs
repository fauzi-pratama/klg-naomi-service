
using apps.Configs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("engine_rule_result")]
    [Index(nameof(Id))]
    public class EngineRuleResult : BaseEntities
    {
        [ForeignKey("engine_rule")]
        [Column("engine_rule_id", Order = 1), MaxLength(50)]
        public string? EngineRuleId { get; set; }

        [Required]
        [Column("group_line", Order = 2)]
        public int GroupLine { get; set; }

        [Required]
        [Column("line_num", Order = 3)]
        public int LineNum { get; set; }

        [Required]
        [Column("item", Order = 4), MaxLength(50)]
        public string? Item { get; set; }

        [Required]
        [Column("value", Order = 5)]
        public string? Value { get; set; }

        [Required]
        [Column("max_value", Order = 6)]
        public string? MaxValue { get; set; }

        [Column("link", Order = 7)]
        public string? Link { get; set; }
    }
}
