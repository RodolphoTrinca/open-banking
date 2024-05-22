using NSubstitute;
using OpenBanking.Tests.Helpers;

namespace OpenBanking.Tests.Repository
{
    public class Remove : BankDataRepositoryBase
    {
        [Fact]
        public void RemoveBankDataFromDatabase()
        {
            var data = new BankDataFactory().Build();

            _context.BankData.Add(data);
            _context.SaveChanges();

            var bankDataList = _context.BankData.ToList();

            Assert.NotNull(bankDataList);

            _repository.Remove(data);

            bankDataList = _context.BankData.ToList();
            Assert.Empty(bankDataList);
        }
    }
}
