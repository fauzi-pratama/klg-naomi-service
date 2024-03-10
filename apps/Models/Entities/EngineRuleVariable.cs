
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("engine_rule_variable")]
    public class EngineRuleVariable
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
        [Column("code", Order = 3)]
        public string? Code { get; set; }

        [Required]
        [Column("name", Order = 4), MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [Column("expression", Order = 5), DataType("text")]
        public string? Expression { get; set; }
    }
}
