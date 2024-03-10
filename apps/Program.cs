
using apps.Configs;
using apps.Helper;
using apps.Models.Contexts;
using apps.Services;
using Microsoft.EntityFrameworkCore;
using VaultSharp.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Config Vault
VaultConfig vaultConfig = builder.Configuration.GetSection("Vault").Get<VaultConfig>() ?? 
    throw new ArgumentNullException("Env Vault Config is null");

if(vaultConfig is not { Link : not null, BasePath : not null, Token: not null, MountPoint: not null })
    throw new ArgumentNullException("Env Vault Config is null or not complete !!");

builder.Configuration.AddJsonFile("appsettings.json").AddVaultConfiguration(() =>
    new VaultOptions(vaultAddress: vaultConfig.Link, vaultToken: vaultConfig.Token), vaultConfig.BasePath, vaultConfig.MountPoint);

//Config Env
builder.Services.Configure<AppConfig>(builder.Configuration);
AppConfig appConfig = builder.Configuration.Get<AppConfig>() ?? 
    throw new ArgumentNullException($"Env Apps is null on Vault path {vaultConfig.MountPoint}{vaultConfig.BasePath}");

//Config Db PostgreSql
if(appConfig.PostgreSqlConnectionString is null)
    throw new ArgumentNullException("Env PostgreSqlConnectionString is null");

builder.Services.AddDbContext<DataDbContext>(options => {
    options.UseNpgsql(appConfig.PostgreSqlConnectionString!);
});

//Setup DI Service
builder.Services.AddSingleton<IEngineService, EngineService>();
builder.Services.AddScoped<EngineSetupWorkflow>();

//Setup Background Service
builder.Services.AddHostedService<EngineSetupWorkflowHelper>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
