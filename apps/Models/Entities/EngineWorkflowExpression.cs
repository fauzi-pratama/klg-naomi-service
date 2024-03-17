
using apps.Configs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("engine_workflow_expression")]
    [Index(nameof(Id))]
    public class EngineWorkflowExpression : BaseEntities
    {
        [ForeignKey("engine_workflow")]
        [Column("engine_workflow_id", Order = 1), MaxLength(50)]
        public string? EngineWorkflowId { get; set; }

        [Required]
        [Column("code", Order = 2), MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [Column("name", Order = 3), MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [Column("expression", Order = 4), DataType("text")]
        public string? Expression { get; set; }
    }
}
