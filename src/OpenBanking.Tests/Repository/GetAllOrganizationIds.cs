using OpenBanking.Application.Entity;
using OpenBanking.Tests.Helpers;

namespace OpenBanking.Tests.Repository
{
    public class GetAllOrganizationIds : BankDataRepositoryBase
    {
        public static IEnumerable<object[]> GetTestParameters()
        {
            var listBankData = new List<BankData>()
            {
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build()
            };

            var expectedResult = listBankData.Select(bd => bd.OrganizationId)
                .ToList();

            yield return new object[] { 
                listBankData,
                expectedResult
            };
        }

        [Theory]
        [MemberData(nameof(GetTestParameters))]
        public void FetchAllOrganizationIDs(List<BankData> listParticipants, List<Guid> expectedResult)
        {
            _context.BankData.AddRange(listParticipants);
            _context.SaveChanges();

            var bankDataList = _context.BankData.ToList();

            Assert.Equal(listParticipants, bankDataList);

            var result = _repository.GetAllOrganizationIds();

            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
        }
    }
}
