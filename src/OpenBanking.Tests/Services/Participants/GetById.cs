using MongoDB.Bson;
using NSubstitute;
using OpenBanking.Application.Entity;
using OpenBanking.Tests.Helpers;

namespace OpenBanking.Tests.Services.Participants
{
    public class GetById : ParticipantsService
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void FetchOrganizationByObjectId(int expectedIndex)
        {
            var participantsIdsList = new List<BankData>()
            {
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build(),
                new BankDataFactory().Build()
            };

            var participantExpected = participantsIdsList[expectedIndex];

            _bankDataRepository.GetById(Arg.Is<ObjectId>(id => id.Equals(participantExpected.Id)))
                .Returns(participantExpected);

            var result = _service.GetById(participantExpected.Id);

            Assert.NotNull(result);
            Assert.Equal(participantExpected, result);
        }
    }
}
