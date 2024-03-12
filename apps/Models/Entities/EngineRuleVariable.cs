
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apps.Models.Entities
{
    [Table("engine_rule_variable")]
    [Index(nameof(EngineRuleCode), nameof(Code))]
    public class EngineRuleVariable
    {
        [ForeignKey("engine_rule")]
        [Column("engine_rule_code", Order = 0), MaxLength(50)]
        public string? EngineRuleCode { get; set; }

        [Key]
        [Required]
        [Column("code", Order = 1), MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [Column("line_num", Order = 2)]
        public int LineNum { get; set; }

        [Required]
        [Column("name", Order = 3), MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [Column("expression", Order = 4), DataType("text")]
        public string? Expression { get; set; }
    }
}
