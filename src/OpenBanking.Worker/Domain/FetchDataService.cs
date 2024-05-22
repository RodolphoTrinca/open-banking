using OpenBanking.Worker.Gateway;

namespace OpenBanking.Worker.FetchData
{
    public class FetchDataService : IFetchDataService
    {
        private readonly IOpenBankingGateway _gateway;
        private readonly IDataProcessor _dataProcessor;
        private readonly ILogger<FetchDataService> _logger;

        public FetchDataService(IOpenBankingGateway gateway, IDataProcessor dataProcessor, ILogger<FetchDataService> logger)
        {
            _gateway = gateway;
            _dataProcessor = dataProcessor;
            _logger = logger;
        }

        public async Task FetchAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("[Fetch Data] Sending request...");
            var response = await _gateway.GetBankDataAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"[Fetch Data] Error to fetch data status code: {response.StatusCode} message:{response.ErrorMessage}");
                return;
            }

            _logger.LogTrace($"[Fetch Data] Response: {response}");
            await _dataProcessor.Process(response.Content);
        }
    }
}
