using MongoDB.Bson;
using OpenBanking.Application.Entity;
using OpenBanking.Application.Interfaces;

namespace OpenBanking.Application.Services
{
    public class BankDataService : IBankDataService
    {
        private readonly IBankDataRepository _repository;

        public BankDataService(IBankDataRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Guid> GetAllOrganizationIds()
        {
            return _repository.GetAllOrganizationIds();
        }

        public IEnumerable<BankData> GetAll(int? skip = null, int? take = null)
        {
            return _repository.GetAll(skip, take);
        }

        public BankData? GetById(ObjectId id)
        {
            return _repository.GetById(id);
        }

        public BankData? GetByOrganizationId(Guid id)
        {
            return _repository.GetOrganizationById(id);
        }

        public void Remove(BankData obj)
        {
            _repository.Remove(obj);
        }

        public void SaveOrUpdate(BankData obj)
        {
            _repository.SaveOrUpdate(obj);
        }
    }
}
