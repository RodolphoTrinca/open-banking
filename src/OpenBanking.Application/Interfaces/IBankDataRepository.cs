using MongoDB.Bson;
using OpenBanking.Application.Entity;

namespace OpenBanking.Application.Interfaces
{
    public interface IBankDataRepository
    {
        IEnumerable<BankData> GetAll(int? skip = null, int? take = null);
        BankData? GetById(ObjectId id);
        BankData? GetOrganizationById(Guid id);
        IEnumerable<Guid> GetAllOrganizationIds();
        void Remove(BankData obj);
        void SaveOrUpdate(BankData obj);
    }
}