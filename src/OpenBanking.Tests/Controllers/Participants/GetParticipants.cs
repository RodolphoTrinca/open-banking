using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenBanking.API.DTO;
using OpenBanking.Application.Entity;
using OpenBanking.Tests.Helpers;

namespace OpenBanking.Tests.Controllers.Participants{
    public class GetParticipants : ParticipantsControllerBase
    {
        [Fact]
        public async void ListParticipants()
        {
            var participantsList = new List<BankData>()
            {
                new BankDataFactory().CreateBankData(),
                new BankDataFactory().CreateBankData(),
                new BankDataFactory().CreateBankData(),
                new BankDataFactory().CreateBankData()
            };

            _participantsService.GetAll(Arg.Any<int>(), Arg.Any<int>())
                .Returns(participantsList);

            var result = await _controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ParticipantsDTO>(okResult.Value);
            Assert.Equal(participantsList, returnValue.Participants);
        }

        [Fact]
        public async Task NoParticipantsFound()
        {
            _participantsService.GetAll(Arg.Any<int>(), Arg.Any<int>())
                .ReturnsForAnyArgs(x => null);

            var result = await _controller.Get();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task HandleExceptionWhenTryGetListOfParticipants()
        {
            var errorMessage = "Error to obtain the list of participants";
            var exception = new Exception(errorMessage);

            _participantsService.GetAll(Arg.Any<int>(), Arg.Any<int>())
                .ReturnsForAnyArgs(x => throw exception);

            var result = await _controller.Get();

            Assert.IsType<BadRequestResult>(result);
            _logger.Received(1).LogError(exception, "Error to get participating banks");
        }
    }
}