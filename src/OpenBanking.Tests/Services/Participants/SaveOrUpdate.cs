using NSubstitute;
using OpenBanking.Application.Entity;
using OpenBanking.Tests.Helpers;

namespace OpenBanking.Tests.Services.Participants
{
    public class SaveOrUpdate : ParticipantsService
    {
        public static IEnumerable<object[]> GetBankData()
        {
            yield return new object[] { new BankDataFactory().Build() };
        }

        [Theory]
        [MemberData(nameof(GetBankData))]
        public void SaveParticipant(BankData item)
        {
            _service.SaveOrUpdate(item);

            _bankDataRepository
                .Received()
                .SaveOrUpdate(Arg.Is<BankData>(bd => bd.Equals(item)));
        }
    }
}
