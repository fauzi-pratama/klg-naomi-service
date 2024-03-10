
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("engine_workflow")]
    public class EngineWorkflow : BaseEntities
    {
        [Key]
        [Column("code", Order = 0), MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [Column("name", Order = 1), MaxLength(200)]
        public string? Name { get; set; }

        public List<EngineWorkflowExpression>? Expression { get; set; }

        public List<EngineRule>? Rule { get; set; }
    }
}
