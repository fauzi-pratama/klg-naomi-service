
using System.Text;
using RulesEngine.Models;
using RulesEngine.Actions;
using apps.Configs;
using apps.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace apps.Services
{
    public interface IEngineService
    {
        bool SetupEngine(Workflow[] dataWorkflow);
        Task<List<RuleResultTree>> GetPromo(string workflowPromo, object findDataPromo, bool getDetail = false);
    }

    public class EngineService() : IEngineService
    {
        private RulesEngine.RulesEngine? _rulesEngine;

        public bool SetupEngine(Workflow[] dataWorkflow)
        {
            var reSettings = new ReSettings
            {
                CustomActions = 
                    new Dictionary<string, Func<ActionBase>>
                    {
                        {"ResultPromo", () => new EngineResult()}
                    }
            };

            _rulesEngine = new RulesEngine.RulesEngine(dataWorkflow, reSettings);

            return true;
        }

        public async Task<List<RuleResultTree>> GetPromo(string workflowPromo, object findDataPromo, bool getDetail = false)
        {
            RuleParameter paramsPromo = new("paramsPromo", findDataPromo);

            List<RuleResultTree> resultList = 
                await _rulesEngine!.ExecuteAllRulesAsync(workflowPromo, paramsPromo);

            if (!getDetail)
                resultList = resultList.Where(q => q.IsSuccess).ToList();

            return resultList;
        }
    }
}
