
using apps.Helper;
using apps.Configs;
using apps.Services;
using System.Reflection;
using StackExchange.Redis;
using apps.Models.Contexts;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using VaultSharp.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Config Vault
VaultConfig vaultConfig = builder.Configuration.GetSection("Vault").Get<VaultConfig>()!;
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddVaultConfiguration(() =>
        new VaultOptions(vaultAddress: vaultConfig.Link, vaultToken: vaultConfig.Token), vaultConfig.BasePath, vaultConfig.MountPoint);

//Config Env
builder.Services.Configure<AppConfig>(builder.Configuration);
AppConfig appConfig = builder.Configuration.Get<AppConfig>()!;

//Config Db PostgreSql
builder.Services.AddDbContext<DataDbContext>(options => {
    options.UseNpgsql(appConfig.PostgreSqlConnectionString!);
});

//Config Redis
builder.Services.AddSingleton<IDatabase>(provider =>
{
    var redisConfig = ConfigurationOptions.Parse(appConfig!.RedisConnectionString!);
    redisConfig.AbortOnConnectFail = false;
    redisConfig.ConnectRetry = 3;
    redisConfig.ConnectTimeout = 5000;
    var redisConn = ConnectionMultiplexer.Connect(redisConfig);

    return redisConn.GetDatabase();
});

//Setup DI Service
builder.Services.AddScoped<EngineSetupWorkflow>();
builder.Services.AddSingleton<IEngineService, EngineService>();
builder.Services.AddScoped<IFindService, FindService>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<ITransService, TransService>();

//Config Automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Setup Background Service
builder.Services.AddHostedService<EngineSetupWorkflowHelper>();

//Config Controller
builder.Services.AddControllers(option =>
{
    option.Filters.Add(typeof(FluentModelResponse));
}).AddFluentValidation(v =>
{
    v.ImplicitlyValidateChildProperties = true;
    v.ImplicitlyValidateRootCollectionElements = true;
    v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

//Setup Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
