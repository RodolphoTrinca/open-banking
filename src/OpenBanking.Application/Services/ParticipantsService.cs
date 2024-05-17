using OpenBanking.Application.Entity;
using OpenBanking.Application.Interfaces;

namespace OpenBanking.Application.Services
{
    public class ParticipantsService : IParticipantsService
    {
        private readonly IBankDataRepository _repository;

        public ParticipantsService(IBankDataRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BankData> GetAll(int skip, int take)
        {
            return _repository.GetAll(skip, take);
        }
    }
}
