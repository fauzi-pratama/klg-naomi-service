
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using apps.Configs;

namespace apps.Models.Entities
{
    [Table("engine_rule_apps")]
    public class EngineRuleApps : BaseEntities
    {
        [Key]
        [Column("Id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("engine_rule")]
        [Column("engine_rule_code", Order = 1), MaxLength(50)]
        public string? EngineRuleCode { get; set; }

        [Required]
        [Column("code", Order = 2)]
        public string? Code { get; set; }

        [Required]
        [Column("name", Order = 3), MaxLength(200)]
        public string? Name { get; set; }
    }
}
