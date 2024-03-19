using apps.Engine.Helpers;
using apps.Engine.Services;

namespace apps.Engine.BackgroundJobs
{
    public class EngineSetupJob(IServiceScopeFactory factory) : BackgroundService
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
