
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("engine_rule_mop")]
    public class EngineRuleMop : BaseEntities
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
        [Column("selection_code", Order = 3)]
        public string? SelectionCode { get; set; }

        [Required]
        [Column("selection_name", Order = 4), MaxLength(200)]
        public string? SelectionName { get; set; }

        [Required]
        [Column("group_code", Order = 5)]
        public string? GroupCode { get; set; }

        [Required]
        [Column("group_name", Order = 6), MaxLength(200)]
        public string? GroupName { get; set; }
    }
}
