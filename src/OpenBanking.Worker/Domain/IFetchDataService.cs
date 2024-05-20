
namespace OpenBanking.Worker.FetchData
{
    public interface IFetchDataService
    {
        Task FetchAsync(string url, CancellationToken cancellationToken);
    }
}