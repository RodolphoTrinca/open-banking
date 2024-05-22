using NSubstitute;
using OpenBanking.Application.Entity;
using OpenBanking.Tests.Helpers;

namespace OpenBanking.Tests.Services.Participants
{
    public class GetAll : ParticipantsService
    {
        public static IEnumerable<object[]> GetTestParameters()
        {
            yield return new object[] {new List<BankData>()
                {
                    new BankDataFactory().Build(),
                    new BankDataFactory().Build(),
                    new BankDataFactory().Build(),
                    new BankDataFactory().Build()
                },
                0,
                100
            };
        }

        [Theory]
        [MemberData(nameof(GetTestParameters))]
        public void GetAllParticipants(List<BankData> participantsList, int skip, int take)
        {
            _bankDataRepository.GetAll(skip, take)
                .Returns(participantsList);

            var result = _service.GetAll(skip, take);

            Assert.NotNull(result);
            Assert.Equal(participantsList, result);
        }
    }
}
