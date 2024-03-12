
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using apps.Configs;
using Microsoft.EntityFrameworkCore;

namespace apps.Models.Entities
{
    [Table("engine_workflow_expression")]
    [Index(nameof(EngineWorkflowCode), nameof(Code))]
    public class EngineWorkflowExpression : BaseEntities
    {
        [ForeignKey("engine_workflow")]
        [Column("engine_workflow_code", Order = 0), MaxLength(50)]
        public string? EngineWorkflowCode { get; set; }

        [Key]
        [Required]
        [Column("code", Order = 1), MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [Column("name", Order = 2), MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [Column("expression", Order = 3), DataType("text")]
        public string? Expression { get; set; }
    }
}
