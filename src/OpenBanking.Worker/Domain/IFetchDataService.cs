
namespace OpenBanking.Worker.FetchData
{
    public interface IFetchDataService
    {
        Task FetchAsync(CancellationToken cancellationToken);
    }
}