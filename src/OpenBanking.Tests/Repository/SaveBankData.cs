using OpenBanking.Tests.Helpers;

namespace OpenBanking.Tests.Repository
{
    public class SaveBankData : BankDataRepositoryBase
    {
        [Fact]
        public void Save()
        {
            var data = new BankDataFactory().Build();

            _repository.SaveOrUpdate(data);

            Assert.NotNull(DbContext.BankData.FirstOrDefault(bd => bd.Id.Equals(data.Id)));
        }
    }
}
