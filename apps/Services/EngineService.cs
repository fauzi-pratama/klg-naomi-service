
using RulesEngine.Models;
using RulesEngine.Actions;
using apps.Configs;
using apps.Models.Response;
using Newtonsoft.Json;
using apps.Models.Request;

namespace apps.Services
{
    public interface IEngineService
    {
        bool SetupEngine(Workflow[] dataWorkflow);
        Task<(List<EngineResponse> data, bool status, string message)> GetPromo(EngineRequest engineRequest, bool getDetail = false);
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

        public async Task<(List<EngineResponse> data, bool status, string message)> GetPromo(EngineRequest engineRequest, bool getDetail = false)
        {
            List<EngineResponse> listPromoResult = [];

            try
            {
                RuleParameter paramsPromo = new("paramsPromo", engineRequest);

                List<RuleResultTree> resultList =
                    await _rulesEngine!.ExecuteAllRulesAsync(engineRequest.CompanyCode, paramsPromo);

                if (!getDetail)
                    resultList = resultList.Where(q => q.IsSuccess).ToList();

                Parallel.ForEach(resultList, loopPromoResult =>
                {
                    var dataResultDetailString = JsonConvert.SerializeObject(loopPromoResult.ActionResult.Output);
                    var dataResultDetail = JsonConvert.DeserializeObject<EngineResponse>(dataResultDetailString);

                    if (dataResultDetail?.PromoCode != null)
                        listPromoResult.Add(dataResultDetail);
                });

                return (listPromoResult, true, "Success");
            }
            catch (Exception ex)
            {
                return (listPromoResult, false, $"Failed get promo from engine : {ex.Message}");
            }
        }
    }
}
