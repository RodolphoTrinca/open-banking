using Microsoft.EntityFrameworkCore;
using OpenBanking.Application.Interfaces;
using OpenBanking.Application.Services;
using OpenBanking.Infra.Context;
using OpenBanking.Infra.Repository;
using OpenBanking.Worker;
using OpenBanking.Worker.Domain;
using OpenBanking.Worker.FetchData;
using Serilog;

try
{
    var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    IConfiguration config = configBuilder.Build();

    var builder = Host.CreateApplicationBuilder(args);

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(config)
        .Enrich.FromLogContext()
        .CreateLogger();

    Log.Information("Starting Worker...");
    Log.Debug("Setting up db context");
    var dataBaseSettings = builder.Configuration.GetSection("OpenBankingStoreDatabase").Get<OpenBankingDatabaseSettings>();

    if (dataBaseSettings == null)
    {
        Log.Error("Error to retrive databaseSettings from appsettings.json");
        Environment.Exit(1);
    }

    builder.Services.Configure<OpenBankingDatabaseSettings>(builder.Configuration.GetSection("OpenBankingStoreDatabase"));
    builder.Services.AddDbContext<OpenBankingDbContext>(options =>
    {
        options.UseMongoDB(dataBaseSettings.ConnectionString ?? "", dataBaseSettings.DatabaseName ?? "");
    });

    Log.Debug("Configuring services");
    builder.Services.AddTransient<IBankDataRepository, BankDataRepository>();
    builder.Services.AddTransient<IBankDataService, BankDataService>();
    builder.Services.AddTransient<IDataProcessor, DataProcessor>();
    builder.Services.AddTransient<IFetchDataService, FetchDataService>();
    builder.Services.AddHostedService<Worker>();

    builder.Services.AddSerilog();

    Log.Debug("Building host");
    var host = builder.Build();

    Log.Information("Starting worker");
    host.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"FATAL ERROR: {ex}");
    Environment.Exit(1);
}
finally
{
    Log.CloseAndFlush();
}


