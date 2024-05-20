using RestSharp;

namespace OpenBanking.Worker.FetchData
{
    public class FetchDataService : IFetchDataService
    {
        private readonly IDataProcessor _dataProcessor;
        private readonly ILogger<FetchDataService> _logger;

        public FetchDataService(IDataProcessor dataProcessor, ILogger<FetchDataService> logger)
        {
            _dataProcessor = dataProcessor;
            _logger = logger;
        }

        public async Task FetchAsync(string url, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(url))
            {
                _logger.LogError("[Fetch Data] The url is null or empty");
                return;
            }

            var options = new RestClientOptions(url);
            var client = new RestClient(options);

            var request = new RestRequest();

            _logger.LogDebug("[Fetch Data] Sending request...");
            var response = await client.GetAsync(request, cancellationToken);

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
