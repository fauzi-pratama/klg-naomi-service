
using apps.Engine.Models.Entities;
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
        public DbSet<EngineWorkflow> EngineWorkflows { get; set; }
        public DbSet<EngineWorkflowExpression> EngineWorkflowExpressions { get; set; }
        public DbSet<EngineRule> EngineRules { get; set; }
        public DbSet<EngineRuleApp> EngineRuleApps { get; set; }
        public DbSet<EngineRuleExpression> EngineRuleExpressions { get; set; }
        public DbSet<EngineRuleMembership> EngineRuleMemberships { get; set; }
        public DbSet<EngineRuleMop> EngineRuleMops { get; set; }
        public DbSet<EngineRuleMopBin> EngineRuleMopBins { get; set; }
        public DbSet<EngineRuleResult> EngineRuleResults { get; set; }
        public DbSet<EngineRuleSite> EngineRuleSites { get; set; }
        public DbSet<EngineRuleVariable> EngineRuleVariables { get; set; }

        //Db Master
        public DbSet<MasterClass> MasterClasses { get; set; }
        public DbSet<MasterEntertainEmail> MasterEntertainEmails { get; set; }
        public DbSet<MasterMop> MasterMops { get; set; }
        public DbSet<MasterSite> MasterSites { get; set; }
        public DbSet<MasterType> MasterTypes { get; set; }
        public DbSet<MasterZone> MasterZones { get; set; }

        //Db Engine Transaction
        public DbSet<PromoMaster> PromoMasters { get; set; }
        public DbSet<PromoTransaction> PromoTransactions { get; set; }
        public DbSet<PromoTransactionDetail> PromoTransactionDetails { get; set; }
    }
}
