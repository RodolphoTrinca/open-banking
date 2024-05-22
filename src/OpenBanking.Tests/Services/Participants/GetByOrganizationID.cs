using NSubstitute;
using OpenBanking.Application.Entity;
using OpenBanking.Tests.Helpers;
namespace OpenBanking.Tests.Services.Participants
{
    public class GetByOrganizationID : ParticipantsService
    {
        public static IEnumerable<object[]> GetTestParameters()
        {
            yield return new object[] {
                new BankDataFactory().Build()
            };
        }

        [Theory]
        [MemberData(nameof(GetTestParameters))]
        public void GetAllParticipants(BankData expectedParticipant)
        {
            _bankDataRepository.GetOrganizationById(Arg.Is<Guid>(id => id.Equals(expectedParticipant.OrganizationId)))
                .Returns(expectedParticipant);

            var result = _service.GetByOrganizationId(expectedParticipant.OrganizationId);

            Assert.NotNull(result);
            Assert.Equal(expectedParticipant, result);
        }
    }
}
