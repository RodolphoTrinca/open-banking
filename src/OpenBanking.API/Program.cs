using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using OpenBanking.Application.Interfaces;
using OpenBanking.Application.Services;
using Microsoft.EntityFrameworkCore;
using OpenBanking.Infra.Context;
using OpenBanking.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Setup mongo db
var dataBaseSettings = builder.Configuration.GetSection("OpenBankingStoreDatabase").Get<OpenBankingDatabaseSettings>();

if (dataBaseSettings == null)
{
    Console.WriteLine("Error to retrive databaseSettings from appsettings.json");
    Environment.Exit(1);
}

builder.Services.Configure<OpenBankingDatabaseSettings>(builder.Configuration.GetSection("OpenBankingStoreDatabase"));
builder.Services.AddDbContext<OpenBankingDbContext>(options =>
{
    options.UseMongoDB(dataBaseSettings.ConnectionString ?? "", dataBaseSettings.DatabaseName ?? "");
});

//Setup SeriLog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddHttpLogging(logging => {
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Open banking API",
        Version = "v1",
        Description = "Open banking API",
    });
});

//Setup dependencies
builder.Services.AddScoped<IBankDataService, BankDataService>();
builder.Services.AddScoped<IBankDataRepository, BankDataRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
app.UseSwagger(options =>
{
    options.RouteTemplate = "api/docs/{documentname}/swagger.json";

    options.PreSerializeFilters.Add((swagger, httpReq) =>
    {
        //Clear servers -element in swagger.json because it got the wrong port when hosted behind reverse proxy
        swagger.Servers.Clear();

    });
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("docs/v1/swagger.json", "Open banking API v1");
    c.RoutePrefix = "api";
});

app.UseRouting();
app.UseHttpLogging();
app.UseAuthorization();
app.MapControllers();

app.Run();
