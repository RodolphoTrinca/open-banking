using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenBanking.API.Web.Controllers;
using OpenBanking.Application.Interfaces;

namespace OpenBanking.Tests.Helpers
{
    public class ParticipantsControllerBase : IDisposable
    {
        protected ParticipantsController _controller;
        protected ILogger<ParticipantsController> _logger;
        protected IBankDataService _participantsService;

        public ParticipantsControllerBase() {
            _controller = CreateParticipantsController();
        }

        protected ParticipantsController CreateParticipantsController(IBankDataService participantsService = null, ILogger<ParticipantsController> logger = null)
        {
            _logger = logger ?? Substitute.For<ILogger<ParticipantsController>>();
            _participantsService = participantsService ?? Substitute.For<IBankDataService>();

            return new ParticipantsController(_participantsService, _logger);
        }

        public void Dispose()
        {
            
        }
    }
}
