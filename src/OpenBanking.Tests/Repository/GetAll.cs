using Microsoft.EntityFrameworkCore;
using OpenBanking.Application.Entity;
using OpenBanking.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBanking.Tests.Repository
{
    public class GetAll : BankDataRepositoryBase
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

            yield return new object[] {
                listBankData,
                0,
                100
            };

            yield return new object[]
            {
                listBankData,
                3,
                3
            };

            yield return new object[]
            {
                listBankData,
                1,
                2
            };
        }

        [Theory]
        [MemberData(nameof(GetTestParameters))]
        public void FetchAllBankData(List<BankData> listParticipants, int skip, int take)
        {
            _context.BankData.AddRange(listParticipants);
            _context.SaveChanges();

            var bankDataList = _context.BankData.ToList();

            Assert.Equal(listParticipants, bankDataList);

            var expectedResult = listParticipants.Skip(skip).Take(take).ToList();

            var result = _repository.GetAll(skip, take);

            Assert.NotNull(result);
            Assert.Equal(expectedResult, result);
        }
    }
}
