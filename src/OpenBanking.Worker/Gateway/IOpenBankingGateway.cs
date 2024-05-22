using RestSharp;

namespace OpenBanking.Worker.Gateway
{
    public interface IOpenBankingGateway
    {
        Task<RestResponse> GetBankDataAsync(CancellationToken cancelationToken);
    }
}