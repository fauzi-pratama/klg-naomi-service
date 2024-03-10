using apps.Models.Contexts;
using apps.Models.Entities;
using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;
using System.Text;

namespace apps.Configs
{
    public class EngineSetupWorkflow(DataDbContext dbContext, ILogger<EngineSetupWorkflow> logger)
    {
        public async Task<Workflow[]> SetupWorkflow()
        {
            List<Workflow> listWorkflow = [];

            List<EngineWorkflow>? listEngineWorkflow =
                await dbContext.EngineWorkflow
                .Where(q => q.ActiveFlag)
                .Include(q => q.Expression)
                .Include(q => q.Rule.Where(q => q.ActiveFlag && q.EndDate >= DateTime.UtcNow))
                    .ThenInclude(q => q.Mops)
                .Include(q => q.Rule.Where(q => q.ActiveFlag && q.EndDate >= DateTime.UtcNow))
                    .ThenInclude(q => q.Variables)
                .Include(q => q.Rule.Where(q => q.ActiveFlag && q.EndDate >= DateTime.UtcNow))
                    .ThenInclude(q => q.Expressions)
                .Include(q => q.Rule.Where(q => q.ActiveFlag && q.EndDate >= DateTime.UtcNow))
                    .ThenInclude(q => q.Results)
                .ToListAsync();

            if (listEngineWorkflow is null || !listEngineWorkflow.Any())
                return [.. listWorkflow];

            Parallel.ForEach(listEngineWorkflow.Where(q => q.Expression is null || q.Expression.Count < 1), loopEngineWorkflow =>
            {
                logger.LogError($"Company {loopEngineWorkflow.Code} has no expression !!");
            });

            Parallel.ForEach(listEngineWorkflow.Where(q => q.Expression is not null && q.Expression.Count > 0), loopEngineWorkflow =>
            {
                //Setup Global params
                var listGlobalParams = new List<ScopedParam>();

                Parallel.ForEach(loopEngineWorkflow.Expression, loopExpression =>
                {
                    listGlobalParams.Add(new ScopedParam()
                    {
                        Name = loopExpression.Code,
                        Expression = loopExpression.Expression
                    });
                });

                //Setup Rule
                var listRule = new List<Rule>();

                if (loopEngineWorkflow.Rule is not null && loopEngineWorkflow.Rule.Any())
                {
                    Parallel.ForEach(loopEngineWorkflow.Rule.Where(q => q.Variables is null || q.Variables.Count < 1 ||
                    q.Expressions is null || q.Expressions.Count < 1), loopEngineRules =>
                    {
                        logger.LogError($"Company {loopEngineWorkflow.Code} And Promo Code {loopEngineRules.Code} has no variables or expressions !!");
                    });

                    Parallel.ForEach(loopEngineWorkflow.Rule.Where(q => q.Variables is not null && q.Variables.Count > 0 &&
                        q.Expressions is not null && q.Expressions.Count > 0), loopEngineRules =>
                        {
                            //Setup Local Params
                            List<ScopedParam> listLocalParams = new();
                            Parallel.ForEach(loopEngineRules.Variables, loopEngineRulesVariable =>
                            {
                                listLocalParams.Add(new ScopedParam()
                                {
                                    Name = loopEngineRulesVariable.Code,
                                    Expression = loopEngineRulesVariable.Expression
                                });
                            });

                            //Setup Expression
                            StringBuilder ruleExp = new();
                            int groupLine = 0, countGroupLine = 0, countGroupLineSave = 0, maxGroupLine = loopEngineRules.Expressions.Max(q => q.GroupLine);

                            foreach (var loopEngineRulesExpression in loopEngineRules.Expressions.OrderBy(q => q.LineNum))
                            {
                                LocalParam localParamas = new()
                                {
                                    Name = loopEngineRulesExpression.Code,
                                    Expression = loopEngineRulesExpression.Expression
                                };

                                listLocalParams.Add(localParamas);

                                var linkExp = loopEngineRulesExpression.Link != null &&
                                                loopEngineRulesExpression.Link != "" ? $" {loopEngineRulesExpression.Link} " : "";

                                if (loopEngineRulesExpression.GroupLine != 0)
                                {
                                    if (loopEngineRulesExpression.GroupLine > groupLine)
                                    {
                                        countGroupLineSave = loopEngineRules.Expressions
                                                            .Count(q => q.GroupLine == loopEngineRulesExpression.GroupLine);
                                        countGroupLine = loopEngineRules.Expressions
                                                            .Count(q => q.GroupLine == loopEngineRulesExpression.GroupLine);

                                        if (loopEngineRulesExpression.GroupLine == 2 && countGroupLine == countGroupLineSave && countGroupLine != 1)
                                        {
                                            ruleExp.Append("((" + loopEngineRulesExpression.Code + linkExp);
                                        }
                                        else if (loopEngineRulesExpression.GroupLine == 2 && countGroupLine == countGroupLineSave && maxGroupLine != loopEngineRulesExpression.GroupLine && countGroupLine == 1)
                                        {
                                            ruleExp.Append("((" + loopEngineRulesExpression.Code + ")" + linkExp);
                                        }
                                        else if (loopEngineRulesExpression.GroupLine == 2 && countGroupLine == countGroupLineSave && maxGroupLine == loopEngineRulesExpression.GroupLine && countGroupLine == 1)
                                        {
                                            ruleExp.Append("((" + loopEngineRulesExpression.Code + "))");
                                        }
                                        else if (loopEngineRulesExpression.GroupLine != 2 && maxGroupLine == loopEngineRulesExpression.GroupLine && countGroupLine == 1)
                                        {
                                            ruleExp.Append('(' + loopEngineRulesExpression.Code + "))");
                                        }
                                        else if (loopEngineRulesExpression.GroupLine != 2 && countGroupLine == 1)
                                        {
                                            ruleExp.Append('(' + loopEngineRulesExpression.Code + ")" + linkExp);
                                        }
                                        else
                                        {
                                            ruleExp.Append('(' + loopEngineRulesExpression.Code + linkExp);
                                        }

                                        groupLine++;
                                        countGroupLine--;
                                    }
                                    else
                                    {
                                        if (maxGroupLine == loopEngineRulesExpression.GroupLine && countGroupLine == 1)
                                        {
                                            ruleExp.Append(loopEngineRules.Cls is 2 or 3 or 4 ? loopEngineRulesExpression.Code + ")" : loopEngineRulesExpression.Code + "))");
                                        }
                                        else if (countGroupLine == 1)
                                        {
                                            ruleExp.Append(loopEngineRulesExpression.Code + ")" + linkExp);
                                        }
                                        else
                                        {
                                            ruleExp.Append(loopEngineRulesExpression.Code + linkExp);
                                        }

                                        countGroupLine--;
                                    }
                                }
                                else
                                {
                                    ruleExp.Append(loopEngineRulesExpression.Code + linkExp);
                                }
                            }

                            //Setup Action
                            EngineRule getDataResult = loopEngineRules;
                            getDataResult.Variables = null;
                            getDataResult.Expressions = null;

                            ActionInfo actionInfo = new()
                            {
                                Name = "ResultPromo",
                                Context = new Dictionary<string, object>()
                            {
                                { "DataPromo", getDataResult }
                            }
                            };

                            RuleActions ruleActions = new()
                            {
                                OnSuccess = actionInfo
                            };

                            //Setup Rules
                            listRule.Add(new Rule()
                            {
                                RuleName = loopEngineRules.Code,
                                LocalParams = listLocalParams,
                                Expression = ruleExp.ToString(),
                                Actions = ruleActions,
                                SuccessEvent = $"{Convert.ToString(loopEngineRules.Cls)}#{Convert.ToString(loopEngineRules.Lvl)}"
                            });
                        });
                }

                //Setup Workflow
                listWorkflow.Add(new Workflow()
                {
                    WorkflowName = loopEngineWorkflow.Code,
                    GlobalParams = listGlobalParams,
                    Rules = listRule
                });

            });

            return [.. listWorkflow];
        }
    }
}
