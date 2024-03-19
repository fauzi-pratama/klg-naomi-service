using apps.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Engine.Models.Entities
{
    [Table("master_entertain_email")]
    [Index(nameof(Id))]
    public class MasterEntertainEmail : BaseEntities
    {
        [Required]
        [Column("nip", Order = 1), MaxLength(50)]
        public string? Nip { get; set; }

        [Required]
        [Column("email", Order = 2)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
