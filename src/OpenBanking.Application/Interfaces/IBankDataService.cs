
using MongoDB.Bson;
using OpenBanking.Application.Entity;

namespace OpenBanking.Application.Interfaces{
    public interface IBankDataService{
        IEnumerable<BankData> GetAll(int? skip = null, int? take = null);
        BankData? GetById(ObjectId id);
        BankData? GetByOrganizationId(Guid id);
        IEnumerable<Guid> GetAllOrganizationIds();
        void Remove(BankData obj);
        void SaveOrUpdate(BankData obj);
    }
}