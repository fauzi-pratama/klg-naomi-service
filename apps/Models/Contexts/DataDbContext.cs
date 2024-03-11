
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
        public DbSet<TransOtp> TransOtp { get; set; }
    }
}
