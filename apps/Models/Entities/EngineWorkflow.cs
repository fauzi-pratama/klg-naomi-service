
using apps.Configs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("engine_workflow")]
    [Index(nameof(Id))]
    public class EngineWorkflow : BaseEntities
    {
        [Column("code", Order = 1), MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [Column("name", Order = 2), MaxLength(200)]
        public string? Name { get; set; }

        public List<EngineWorkflowExpression>? Expressions { get; set; }

        public List<EngineRule>? Rules { get; set; }
    }
}
