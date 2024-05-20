namespace OpenBanking.Worker.FetchData
{
    public interface IDataProcessor
    {
        Task Process(string data);
    }
}