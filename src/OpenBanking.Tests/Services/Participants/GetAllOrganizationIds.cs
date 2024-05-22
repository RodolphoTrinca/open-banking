using NSubstitute;
using OpenBanking.Application.Entity;
using OpenBanking.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBanking.Tests.Services.Participants
{
    public class GetAllOrganizationIds : ParticipantsService
    {
        [Fact]
        public void GetAllParticipants()
        {
            var participantsIdsList = new List<Guid>()
            {
                new BankDataFactory().Build().OrganizationId,
                new BankDataFactory().Build().OrganizationId,
                new BankDataFactory().Build().OrganizationId,
                new BankDataFactory().Build().OrganizationId
            };

            _bankDataRepository.GetAllOrganizationIds()
                .Returns(participantsIdsList);

            var result = _service.GetAllOrganizationIds();

            Assert.NotNull(result);
            Assert.Equal(participantsIdsList, result);
        }
    }
}
