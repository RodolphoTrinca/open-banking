
using OpenBanking.Application.Entity;

namespace OpenBanking.Application.Interfaces{
    public interface IParticipantsService{
        IEnumerable<BankData> GetAll(int skip, int take);
    }
}