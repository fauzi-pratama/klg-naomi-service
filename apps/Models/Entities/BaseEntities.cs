﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    public class BaseEntities
    {
        [Required]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("created_by"), MaxLength(50)]
        public string? CreatedBy { get; set; } = "SYSTEM";

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }

        [Column("updated_by"), MaxLength(50)]
        public string? UpdatedBy { get; set; }

        [Required]
        [Column("active_flag")]
        public bool ActiveFlag { get; set; } = true;
    }
}
