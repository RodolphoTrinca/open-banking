using RestSharp;

namespace OpenBanking.Worker.Gateway
{
    public class OpenBankingGateway : IOpenBankingGateway
    {
        private RestClient _client;
        private ILogger<OpenBankingGateway> _logger;

        public OpenBankingGateway(IConfiguration config, ILogger<OpenBankingGateway> logger)
        {
            _logger = logger;
            var url = config["FetchURL"] ?? string.Empty;

            if (string.IsNullOrEmpty(url))
            {
                _logger.LogError("The fetch url is null or empty");
                return;
            }

            _logger.LogDebug($"url to fetch: {url}");
            _client = new RestClient(url);
        }

        public async Task<RestResponse> GetBankDataAsync(CancellationToken cancelationToken)
        {
            var request = new RestRequest();

            _logger.LogDebug("Requesting data from open banking api");
            return await _client.GetAsync(request, cancelationToken);
        }
    }
}
