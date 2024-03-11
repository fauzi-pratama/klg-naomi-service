
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using apps.Configs;

namespace apps.Models.Entities
{
    [Table("engine_rule_result")]
    public class EngineRuleResult : BaseEntities
    {
        [Key]
        [Column("Id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("engine_rule")]
        [Column("engine_rule_code", Order = 1), MaxLength(50)]
        public string? EngineRuleCode { get; set; }

        [Required]
        [Column("line_num", Order = 2)]
        public int LineNum { get; set; }

        [Required]
        [Column("group_line", Order = 3)]
        public int GroupLine { get; set; }

        [Required]
        [Column("item", Order = 4)]
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
