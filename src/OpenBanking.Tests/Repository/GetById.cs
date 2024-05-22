using OpenBanking.Application.Entity;
using OpenBanking.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBanking.Tests.Repository
{
    public class GetById : BankDataRepositoryBase
    {
        public static IEnumerable<object[]> GetTestParameters()
        {
            var expectedResult = new BankDataFactory().Build();
            yield return new object[] {new List<BankData>()
                {
                    new BankDataFactory().Build(),
                    new BankDataFactory().Build(),
                    new BankDataFactory().Build(),
                    new BankDataFactory().Build(),
                    new BankDataFactory().Build(),
                    new BankDataFactory().Build(),
                    expectedResult,
                    new BankDataFactory().Build(),
                    new BankDataFactory().Build(),
                    new BankDataFactory().Build(),
                    new BankDataFactory().Build(),
                    new BankDataFactory().Build(),
                    new BankDataFactory().Build()
                },
                expectedResult
            };
        }

        [Theory]
        [MemberData(nameof(GetTestParameters))]
        public void FetchBankDataByObjectID(List<BankData> listParticipants, BankData expectedResult)
        {
            _context.BankData.AddRange(listParticipants);
            _context.SaveChanges();

            var bankDataList = _context.BankData.ToList();

            Assert.Equal(listParticipants, bankDataList);

            var result = _repository.GetById(expectedResult.Id);

            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
        }
    }
}
