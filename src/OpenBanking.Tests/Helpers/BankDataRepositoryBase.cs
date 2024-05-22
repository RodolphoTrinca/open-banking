using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenBanking.Infra.Repository;

namespace OpenBanking.Tests.Helpers
{
    public class BankDataRepositoryBase : DbFixture
    {
        protected ILogger<BankDataRepository> _logger;
        protected BankDataRepository _repository;

        public BankDataRepositoryBase() : base()
        {
            _logger = Substitute.For<ILogger<BankDataRepository>>();
            _repository = new BankDataRepository(_context, _logger);
        }
    }
}
