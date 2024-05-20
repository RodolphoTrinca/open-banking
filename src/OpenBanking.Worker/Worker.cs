using OpenBanking.Worker.FetchData;

namespace OpenBanking.Worker
{
    public sealed class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;

        private const string ClassName = nameof(Worker);

        public Worker(IServiceScopeFactory serviceScopeFactory, ILogger<Worker> logger, IConfiguration configuration)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _config = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Running Worker");
            var url = _config["FetchURL"] ?? string.Empty;
            var configSleepTime = _config.GetValue<int>("CooldownTime");
            var sleepTime = configSleepTime == 0 ? 60000 : configSleepTime;

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IFetchDataService>();

                    if (service == null)
                    {
                        _logger.LogError($"Failed to retrive the {nameof(IFetchDataService)} service");
                        return;
                    }

                    _logger.LogInformation("Fetching data");
                    _logger.LogDebug($"url to fetch: {url}");
                    await service.FetchAsync(url, stoppingToken);
                }

                _logger.LogInformation($"Sleeping for: {sleepTime}ms");
                await Task.Delay(sleepTime, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("{Name} is stopping.", ClassName);

            await base.StopAsync(stoppingToken);
        }
    }
}
