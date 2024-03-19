using apps.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Engine.Models.Entities
{
    [Table("engine_rule")]
    [Index(nameof(Id))]
    public class EngineRule : BaseEntities
    {
        [ForeignKey("engine_workflow")]
        [Column("engine_workflow_id", Order = 1), MaxLength(50)]
        public string? EngineWorkflowId { get; set; }

        [Column("code", Order = 2)]
        public string? Code { get; set; }

        [Column("redeem_code", Order = 3), MaxLength(50)]
        public string? RedeemCode { get; set; }

        [Required]
        [Column("name", Order = 4), MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [Column("cls", Order = 5)]
        public int Cls { get; set; } //Untuk Pengkelompokan Promo (1 = Item, 2 = Chart, 3 = Mop, 4 = Entertaint)

        [Required]
        [Column("lvl", Order = 6)]
        public int Lvl { get; set; } //Untuk Execute Promo di Class 1 (1 = ITEM, ITEMVALUE, SP)

        [Required]
        [Column("item_type", Order = 7), MaxLength(50)]
        public string? ItemType { get; set; } //Untuk Result Promo (ALL = Semua Item di Cart, CUSTOM = Sesuai Dengan tabel Result Item)

        [Required]
        [Column("result_type", Order = 8), MaxLength(50)]
        public string? ResultType { get; set; } //Execute Promo Result Sebagian atau Semua (V1 = Harus Ada Semua Result di Cart, V2 = Tidak Harus Ada Semua Result di Cart)

        [Required]
        [Column("action_type", Order = 9), MaxLength(50)]
        public string? ActionType { get; set; } //Tipe Promo (ITEM, ITEMVALUE, AMOUNT, PERCENT, SP, BUNDLE)

        [Column("action_value", Order = 10), DataType("text")]
        public string? ActionValue { get; set; } //Jika Item Type ALL Maka Value Discount Di isi , Contoh Discount Amount atau Discount Percent

        [Column("max_value", Order = 11), MaxLength(50)]
        public string? MaxValue { get; set; } //Value Max Discount Percent untuk Item Type ALL

        [Column("max_multiple", Order = 12)]
        public int? MaxMultiple { get; set; } //Promo Bertingkat Seperti Beli 10 Gratis 1 Beli 20 Gratus 2

        [Column("max_use", Order = 13)]
        public int? MaxUse { get; set; } //Quota SoftBooking Penggunaan Promo Base on Qty

        [Column("max_balance", Order = 14)]
        public decimal? MaxBalance { get; set; } //Quota SoftBooking Penggunaan Promo Base on Amount

        [Column("start_date", Order = 15)]
        public DateTime StartDate { get; set; } //Tanggal Promo aktif

        [Column("end_date", Order = 16)]
        public DateTime EndDate { get; set; } //Tanggal Promo Berhenti

        [Column("ref_code", Order = 17), MaxLength(50)]
        public string? RefCode { get; set; } //Refrerence Code Untuk Promo Bertingkat

        [Column("multiple_qty", Order = 18)]
        public int MultipleQty { get; set; } //Urutan Lv Promo Bertingkat

        [Column("max_qty", Order = 19)]
        public int MaxQty { get; set; } //Promo Di Apply Ke Beberapa Qty Item

        [Column("entertaiment_nip", Order = 20), MaxLength(50)]
        public string? EntertaimentNip { get; set; }

        [Column("description", Order = 21), DataType("text")]
        public string? Description { get; set; }

        [Column("terms_condition", Order = 22), DataType("text")]
        public string? TermsCondition { get; set; }

        [Column("show_flag", Order = 23)]
        public bool ShowFlag { get; set; } = false; //Promo yang akan di show ke displayed

        [Column("member_flag", Order = 24)]
        public bool MemberFlag { get; set; }

        [Column("new_member_flag", Order = 25)]
        public bool NewMemberFlag { get; set; }

        [Column("member_quota_periode", Order = 26)]
        public int MemberQuotaPeriode { get; set; } //Quota Promo Untuk Setiap Periode

        [Column("member_repeat_periode", Order = 27)] //Lama Periode Promo
        public int MemberRepeatPeriode { get; set; }

        [Column("image_link", Order = 28), DataType("text")]
        public string? ImageLink { get; set; }

        [Column("absolute_flag ", Order = 29)]
        public bool AbsoluteFlag { get; set; } = true; //Status Promo yang tidak bisa di gabung

        [Column("voucher_flag ", Order = 30)]
        public bool VoucherFlag { get; set; }

        [Column("voucher_code", Order = 31)]
        public string? VoucherCode { get; set; }

        public List<EngineRuleApp>? Apps { get; set; }

        public List<EngineRuleExpression>? Expressions { get; set; }

        public List<EngineRuleMembership>? Memberships { get; set; }

        public List<EngineRuleMop>? Mops { get; set; }

        public List<EngineRuleResult>? Results { get; set; }

        public List<EngineRuleSite>? Sites { get; set; }

        public List<EngineRuleVariable>? Variables { get; set; }

    }
}
