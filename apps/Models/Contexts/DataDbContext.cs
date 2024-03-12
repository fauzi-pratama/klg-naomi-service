
using apps.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace apps.Models.Contexts
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        //Db Engine Setup
        public DbSet<EngineWorkflow>? EngineWorkflow { get; set; }
        public DbSet<EngineWorkflowExpression>? EngineWorkflowExpression { get; set; }
        public DbSet<EngineRule>? EngineRule { get; set; }
        public DbSet<EngineRuleApps>? EngineRuleApps { get; set; }
        public DbSet<EngineRuleExpression>? EngineRuleExpression { get; set; }
        public DbSet<EngineRuleMembership>? EngineRuleMembership { get; set; }
        public DbSet<EngineRuleMop>? EngineRuleMop { get; set; }
        public DbSet<EngineRuleResult>? EngineRuleResult { get; set; }
        public DbSet<EngineRuleSite>? EngineRuleSite { get; set; }
        public DbSet<EngineRuleVariable>? EngineRuleVariable { get; set; }

        //Db Engine Transaction
        public DbSet<PromoTrans> PromoTrans { get; set; }
        public DbSet<PromoTransDetail> PromoTransDetail { get; set; }
        public DbSet<PromoOtp> TransOtp { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Setup Multiple Primary Key 
            modelBuilder.Entity<EngineWorkflowExpression>().HasKey(x => new { x.EngineWorkflowCode, x.Code });
            modelBuilder.Entity<EngineRule>().HasKey(x => new { x.EngineWorkflowCode, x.Code });
            modelBuilder.Entity<EngineRuleApps>().HasKey(x => new { x.EngineRuleCode, x.Code });
            modelBuilder.Entity<EngineRuleExpression>().HasKey(x => new { x.EngineRuleCode, x.Code });
            modelBuilder.Entity<EngineRuleMembership>().HasKey(x => new { x.EngineRuleCode, x.Code });
            modelBuilder.Entity<EngineRuleMop>().HasKey(x => new { x.EngineRuleCode, x.SelectionCode, x.GroupCode });
            modelBuilder.Entity<EngineRuleResult>().HasKey(x => new { x.EngineRuleCode, x.GroupLine, x.LineNum });
            modelBuilder.Entity<EngineRuleSite>().HasKey(x => new { x.EngineRuleCode, x.Code });
            modelBuilder.Entity<EngineRuleVariable>().HasKey(x => new { x.EngineRuleCode, x.Code });
            modelBuilder.Entity<PromoTransDetail>().HasKey(x => new { x.PromoTransId, x.PromoCode });
        }
    }
}
