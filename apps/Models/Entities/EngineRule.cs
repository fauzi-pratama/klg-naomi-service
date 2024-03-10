
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apps.Models.Entities
{
    [Table("engine_rule")]
    public class EngineRule : BaseEntities
    {
        [Key]
        [Column("code", Order = 0)]
        public string? Code { get; set; }

        [ForeignKey("engine_workflow")]
        [Column("engine_workflow_code", Order = 1), MaxLength(50)]
        public string? EngineWorkflowCode { get; set; }

        [Column("redeem_code", Order = 2), MaxLength(50)]
        public string? RedeemCode { get; set; }

        [Required]
        [Column("name", Order = 3), MaxLength(200)]
        public string? Name { get; set; }

        [Required]
        [Column("cls", Order = 4)]
        public int Cls { get; set; } //Untuk Pengkelompokan Promo (1 = Item, 2 = Chart, 3 = Mop, 4 = Entertaint)

        [Required]
        [Column("lvl", Order = 5)]
        public int Lvl { get; set; } //Untuk Execute Promo di Class 1 (1 = ITEM, ITEMVALUE, SP)

        [Required]
        [Column("item_type", Order = 6), MaxLength(50)]
        public string? ItemType { get; set; } //Untuk Result Promo (ALL = Semua Item di Cart, CUSTOM = Sesuai Dengan tabel Result Item)

        [Required]
        [Column("result_type", Order = 7), MaxLength(50)]
        public string? ResultType { get; set; } //Execute Promo Result Sebagian atau Semua (V1 = Harus Ada Semua Result di Cart, V2 = Tidak Harus Ada Semua Result di Cart)

        [Required]
        [Column("action_type", Order = 8), MaxLength(50)]
        public string? ActionType { get; set; } //Tipe Promo (ITEM, ITEMVALUE, AMOUNT, PERCENT, SP, BUNDLE)

        [Column("action_value", Order = 9), DataType("text")]
        public string? ActionValue { get; set; } //Jika Item Type ALL Maka Value Discount Di isi , Contoh Discount Amount atau Discount Percent

        [Column("max_value", Order = 10), MaxLength(50)]
        public string? MaxValue { get; set; } //Value Max Discount Percent untuk Item Type ALL

        [Column("max_multiple", Order = 11)]
        public int? MaxMultiple { get; set; } //Promo Bertingkat Seperti Beli 10 Gratis 1 Beli 20 Gratus 2

        [Column("max_use", Order = 12)]
        public int? MaxUse { get; set; } //Quota SoftBooking Penggunaan Promo Base on Qty

        [Column("max_balance", Order = 13)]
        public decimal? MaxBalance { get; set; } //Quota SoftBooking Penggunaan Promo Base on Amount

        [Column("start_date", Order = 14)]
        public DateTime StartDate { get; set; } //Tanggal Promo aktif

        [Column("end_date", Order = 15)]
        public DateTime EndDate { get; set; } //Tanggal Promo Berhenti

        [Column("ref_code", Order = 16), MaxLength(50)]
        public string? RefCode { get; set; } //Refrerence Code Untuk Promo Bertingkat

        [Column("multiple_qty", Order = 17)]
        public int MultipleQty { get; set; } //Urutan Lv Promo Bertingkat

        [Column("max_qty", Order = 18)]
        public int MaxQty { get; set; } //Promo Di Apply Ke Beberapa Qty Item

        [Column("entertaiment_nip", Order = 19), MaxLength(50)]
        public string? EntertaimentNip { get; set; } 

        [Column("description", Order = 20), DataType("text")]
        public string? Description { get; set; }

        [Column("terms_condition", Order = 21), DataType("text")]
        public string? TermsCondition { get; set; }

        [Column("show_status", Order = 22)]
        public bool ShowStatus { get; set; } = false; //Promo yang akan di show ke displayed

        [Column("member_status", Order = 23)]
        public bool MemberStatus { get; set; } 

        [Column("member_new_status", Order = 24)]
        public bool MemberNewStatus { get; set; }

        [Column("member_quota_periode", Order = 25)]
        public int MemberQuotaPeriode { get; set; } //Quota Promo Untuk Setiap Periode

        [Column("member_repeat_periode", Order = 26)] //Lama Periode Promo
        public int MemberRepeatPeriode { get; set; }

        [Column("image_link", Order = 27), DataType("text")]
        public string? ImageLink { get; set; }

        [Column("absolute_status ", Order = 28)]
        public bool AbsoluteStatus { get; set; } = true; //Status Promo yang tidak bisa di gabung

        [Column("voucherStatus ", Order = 29)]
        public bool VoucherStatus { get; set; }

        [Column("voucher_code", Order = 30)]
        public string? VoucherCode { get; set; }

        public List<EngineRuleApps>? Apps { get; set; }

        public List<EngineRuleExpression>? Expressions { get; set; }

        public List<EngineRuleMembership>? Memberships { get; set; }

        public List<EngineRuleMop>? Mops { get; set; }

        public List<EngineRuleResult>? Results { get; set; }

        public List<EngineRuleSite>? Sites { get; set; }

        public List<EngineRuleVariable>? Variables { get; set; }

    }
}
