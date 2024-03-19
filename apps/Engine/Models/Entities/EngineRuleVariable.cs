using apps.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Engine.Models.Entities
{
    [Table("engine_rule_variable")]
    [Index(nameof(Id))]
    public class EngineRuleVariable : BaseEntities
    {
        [ForeignKey("engine_rule")]
        [Column("engine_rule_id", Order = 1), MaxLength(50)]
        public string? EngineRuleId { get; set; }

        [Required]
        [Column("code", Order = 2), MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [Column("line_num", Order = 3)]
        public int LineNum { get; set; }

        [Required]
        [Column("name", Order = 4), MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [Column("expression", Order = 5), DataType("text")]
        public string? Expression { get; set; }
    }
}
