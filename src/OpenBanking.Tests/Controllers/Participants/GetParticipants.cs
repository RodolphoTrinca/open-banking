using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenBanking.Tests.Helpers;
using Xunit;

namespace OpenBanking.Tests.Controllers.Participants{
    public class GetParticipants : ParticipantsControllerBase
    {
        //[Fact]
        //public async void ListParticipants() {

        //    var result = await _controller.Get();

        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var returnValue = Assert.IsType<ParticipantsDTO>(okResult.Value);
        //}

        [Fact]
        public async Task NoParticipantsFound()
        {
            _participantsService.GetAll(default, default)
                .ReturnsForAnyArgs(x => null);

            var result = await _controller.Get();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ThrowExceptionWhenTryGetListOfParticipants()
        {
            var errorMessage = "Error to obtain the list of participants";
            var exception = new Exception(errorMessage);

            _participantsService.GetAll(default, default)
                .ReturnsForAnyArgs(x => throw exception);

            var result = await _controller.Get();

            Assert.IsType<BadRequestResult>(result);
            _logger.Received(1).LogError(exception, "Error to get participating banks");
        }
    }
}