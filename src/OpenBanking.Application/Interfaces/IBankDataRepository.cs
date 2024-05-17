using MongoDB.Bson;
using OpenBanking.Application.Entity;

namespace OpenBanking.Application.Interfaces
{
    public interface IBankDataRepository
    {
        IEnumerable<BankData> GetAll(int skip = 0, int take = 10);
        BankData? GetById(ObjectId id);
        void Remove(BankData obj);
        void SaveOrUpdate(BankData obj);
    }
}