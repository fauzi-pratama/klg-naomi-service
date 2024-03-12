
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using apps.Configs;
using Microsoft.EntityFrameworkCore;

namespace apps.Models.Entities
{
    [Table("engine_rule_site")]
    [Index(nameof(EngineRuleCode), nameof(Code), nameof(Name))]
    public class EngineRuleSite : BaseEntities
    {
        [ForeignKey("engine_rule")]
        [Column("engine_rule_code", Order = 0), MaxLength(50)]
        public string? EngineRuleCode { get; set; }

        [Key]
        [Required]
        [Column("code", Order = 1), MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [Column("name", Order = 2), MaxLength(200)]
        public string? Name { get; set; }
    }
}
