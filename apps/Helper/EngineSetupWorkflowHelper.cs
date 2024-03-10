﻿
using apps.Configs;
using apps.Services;

namespace apps.Helper
{
    public class EngineSetupWorkflowHelper(IServiceScopeFactory factory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await using AsyncServiceScope asyncScope = factory.CreateAsyncScope();
            EngineSetupWorkflow engineSetupWorkflow = asyncScope.ServiceProvider.GetRequiredService<EngineSetupWorkflow>();
            IEngineService engineService = asyncScope.ServiceProvider.GetRequiredService<IEngineService>();

            engineService.SetupEngine(await engineSetupWorkflow.SetupWorkflow());
        }
    }
}
