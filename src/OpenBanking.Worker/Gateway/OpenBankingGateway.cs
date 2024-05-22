using RestSharp;

namespace OpenBanking.Worker.Gateway
{
    public class OpenBankingGateway : IOpenBankingGateway
    {
        private RestClient _client;

        public OpenBankingGateway(string url)
        {
            _client = new RestClient(url);
        }

        public async Task<RestResponse> GetBankDataAsync(CancellationToken cancelationToken)
        {
            var request = new RestRequest();
            return await _client.GetAsync(request, cancelationToken);
        }
    }
}
