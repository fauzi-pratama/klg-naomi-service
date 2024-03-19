using apps.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Engine.Models.Entities
{
    [Table("master_type")]
    [Index(nameof(Id))]
    public class MasterType : BaseEntities
    {
        [ForeignKey("master_class")]
        [Column("master_class_id", Order = 1), MaxLength(50)]
        public Guid? MasterClassId { get; set; }

        [Required]
        [Column("code", Order = 2), MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [Column("name", Order = 3), MaxLength(200)]
        public string? Name { get; set; }
    }
}
