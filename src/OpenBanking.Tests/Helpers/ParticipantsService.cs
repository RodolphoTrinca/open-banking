using NSubstitute;
using OpenBanking.Application.Interfaces;
using Service = OpenBanking.Application.Services.BankDataService;

namespace OpenBanking.Tests.Helpers
{
    public class ParticipantsService
    {
        protected IBankDataRepository _bankDataRepository;
        protected Service _service;

        public ParticipantsService()
        {
            CreateService();
        }

        protected void CreateService(IBankDataRepository bankDataRepository = null)
        {
            _bankDataRepository = bankDataRepository ?? Substitute.For<IBankDataRepository>();

            _service = new Service(_bankDataRepository);
        }
    }
}
