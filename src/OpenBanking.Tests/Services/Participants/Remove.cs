using NSubstitute;
using OpenBanking.Application.Entity;
using OpenBanking.Tests.Helpers;
using System.Collections;

namespace OpenBanking.Tests.Services.Participants
{
    public class Remove : ParticipantsService
    {
        public static IEnumerable<object[]> GetBankData()
        {
            yield return new object[] { new BankDataFactory().Build() };
        }

        [Theory]
        [MemberData(nameof(GetBankData))]
        public void RemoveParticipant(BankData item)
        {
            _service.Remove(item);

            _bankDataRepository
                .Received()
                .Remove(Arg.Is<BankData>(bd => bd.Equals(item)));
        }
    }
}
