using NSubstitute;
using OpenBanking.Application.Entity;
using OpenBanking.Tests.Helpers;

namespace OpenBanking.Tests.Services.Participants
{
    public class GetAll : ParticipantsService
    {
        [Theory]
        [InlineData(0, 100)]
        public void GetAllParticipants(int skip, int take)
        {
            var participantsList = new List<BankData>()
            {
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build()
            };

            _bankDataRepository.GetAll(skip, take)
                .Returns(participantsList);

            var result = _service.GetAll(skip, take);

            Assert.NotNull(result);
            Assert.Equal(participantsList, result);
        }
    }
}
