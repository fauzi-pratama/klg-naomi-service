﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using apps.Configs;
using Microsoft.EntityFrameworkCore;

namespace apps.Models.Entities
{
    [Table("engine_rule_expression")]
    [Index(nameof(EngineRuleCode), nameof(Code))]
    public class EngineRuleExpression : BaseEntities
    {
        [ForeignKey("engine_rule")]
        [Column("engine_rule_code", Order = 1), MaxLength(50)]
        public string? EngineRuleCode { get; set; }

        [Key]
        [Required]
        [Column("code", Order = 2)]
        public string? Code { get; set; }

        [Required]
        [Column("line_num", Order = 3)]
        public int LineNum { get; set; }

        [Required]
        [Column("group_line", Order = 4)]
        public int GroupLine { get; set; }

        [Required]
        [Column("name", Order = 5), MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [Column("expression", Order = 6), DataType("text")]
        public string? Expression { get; set; }

        [Column("link", Order = 7), MaxLength(5)]
        public string? Link { get; set; }
    }
}
