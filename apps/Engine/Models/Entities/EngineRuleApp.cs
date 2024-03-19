using apps.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Engine.Models.Entities
{
    [Table("engine_rule_app")]
    [Index(nameof(Id))]
    public class EngineRuleApp : BaseEntities
    {
        [ForeignKey("engine_rule")]
        [Column("engine_rule_id", Order = 1), MaxLength(50)]
        public string? EngineRuleId { get; set; }

        [Required]
        [Column("code", Order = 2)]
        public string? Code { get; set; }

        [Required]
        [Column("name", Order = 3), MaxLength(200)]
        public string? Name { get; set; }
    }
}
