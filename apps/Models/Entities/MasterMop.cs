
using apps.Configs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("master_mop")]
    [Index(nameof(Id))]
    public class MasterMop : BaseEntities
    {
        [Required]
        [Column("company_code", Order = 1), MaxLength(50)]
        public string? CompanyCode { get; set; }

        [Required]
        [Column("site_code", Order = 2), MaxLength(50)]
        public string? SiteCode { get; set; }

        [Required]
        [Column("code", Order = 3), MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [Column("name", Order = 4), MaxLength(200)]
        public string? Name { get; set; }
    }
}
