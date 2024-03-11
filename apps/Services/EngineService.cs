
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
        (bool status, string message) SetupEngine(Workflow[] dataWorkflow);
        Task<(List<EngineResponse> data, bool status, string message)> GetPromo(EngineRequest engineRequest, bool getDetail = false);
    }

    public class EngineService() : IEngineService
    {
        private RulesEngine.RulesEngine? _rulesEngine;

        public (bool status, string message) SetupEngine(Workflow[] dataWorkflow)
        {
            try
            {
                //Setup Function Calculate Promo on Engine
                var reSettings = new ReSettings
                {
                    CustomActions =
                        new Dictionary<string, Func<ActionBase>>
                        {
                        {"ResultPromo", () => new EngineResult()}
                        }
                };

                //Setup Variable GLobal Engine
                _rulesEngine = new RulesEngine.RulesEngine(dataWorkflow, reSettings);

                return (true, "Success");
            } catch (Exception ex)
            {
                return (false, $"Failed setup engine : {ex.Message}");
            }
        }

        public async Task<(List<EngineResponse> data, bool status, string message)> GetPromo(EngineRequest engineRequest, bool getDetail = false)
        {
            List<EngineResponse> listPromoResult = [];

            try
            {
                //Setup Parameter Request to Parameter Engine
                RuleParameter paramsPromo = new("paramsPromo", engineRequest); 

                //Running Engine Search Promo
                List<RuleResultTree> resultList =
                    await _rulesEngine!.ExecuteAllRulesAsync(engineRequest.CompanyCode, paramsPromo);

                //Filter Result Engine With All Result or Just Success Result
                if (!getDetail)
                    resultList = resultList.Where(q => q.IsSuccess).ToList();

                //Convert Result Engine to Model EngineResponse
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
