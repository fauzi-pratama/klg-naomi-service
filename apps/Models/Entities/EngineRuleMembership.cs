using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using apps.Configs;

namespace apps.Models.Entities
{
    [Table("engine_rule_membership")]
    public class EngineRuleMembership : BaseEntities
    {
        [Key]
        [Column("Id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("engine_rule")]
        [Column("engine_rule_code", Order = 1), MaxLength(50)]
        public string? EngineRuleCode { get; set; }

        [Required]
        [Column("name", Order = 2), MaxLength(200)]
        public string? Name { get; set; }
    }
}
