using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenBanking.Infra.Repository;
using OpenBanking.Worker.Domain;
using OpenBanking.Worker.FetchData;
using OpenBanking.Worker.Gateway;
using RestSharp;

namespace OpenBanking.Tests.Worker
{
    public class FetchData
    {
        private IOpenBankingGateway _gateway;
        private IDataProcessor _dataProcessor;
        private ILogger<FetchDataService> _logger;
        private FetchDataService _worker;

        public FetchData()
        {
            _gateway = Substitute.For<IOpenBankingGateway>();
            _dataProcessor = Substitute.For<IDataProcessor>();
            _logger = Substitute.For<ILogger<FetchDataService>>();
            _worker = new FetchDataService(_gateway, _dataProcessor, _logger);
        }

        //[Fact]
        public async Task FetchDataFromAPI()
        {
            var cancelationToken = new CancellationToken();
            var data = @"{""payload"": ""response""}";

            _gateway.GetBankDataAsync(
                Arg.Is<CancellationToken>(ct => ct.Equals(cancelationToken)))
                .Returns(x => Task.FromResult(new RestResponse()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = data
            }));

            await _worker.FetchAsync(cancelationToken);

            await _dataProcessor.Received().Process(data);
        }
    }
}
