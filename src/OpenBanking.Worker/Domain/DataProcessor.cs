using OpenBanking.Application.Interfaces;
using OpenBanking.Worker.DTO;
using OpenBanking.Worker.FetchData;
using OpenBanking.Worker.Helper;
using System.Text.Json;

namespace OpenBanking.Worker.Domain
{
    public class DataProcessor : IDataProcessor
    {
        private readonly ILogger<DataProcessor> _logger;
        private readonly IBankDataService _service;

        public DataProcessor(IBankDataService service, ILogger<DataProcessor> logger)
        {
            _logger = logger;
            _service = service;
        }

        public async Task Process(string data)
        {
            _logger.LogInformation("Processing data");
            _logger.LogDebug($"Payload received: {data}");

            var objectResult = JsonSerializer.Deserialize<List<CompanyData>>(data);
            await SaveData(objectResult);
        }

        private async Task SaveData(List<CompanyData> data)
        {
            if (data == null || !data.Any())
            {
                _logger.LogWarning("[Data processor] The data is null or empty");
                return;
            }

            var tasks = new List<Task>();

            foreach (var item in data)
            {
                var task = Task.Factory.StartNew(() => {
                    if (!Guid.TryParse(item.OrganisationId, out var id))
                    {
                        _logger.LogError($"The Organisation id: {item.OrganisationId} cannot be converted to GUID");
                        return;
                    }

                    _logger.LogDebug("Search for bank data");
                    var bankData = _service.GetById(id);

                    if (bankData == null)
                    {
                        _logger.LogDebug($"Bank {item.OrganisationName} not found, saving data...");
                        _service.SaveOrUpdate(item.ToBankData());
                        _logger.LogInformation($"Bank {item.OrganisationName} saved with success!");
                        return;
                    }

                    _logger.LogDebug($"Bank {item.OrganisationName} found, updating data...");
                    bankData.UpdateData(item);
                    _service.SaveOrUpdate(bankData);

                    _logger.LogInformation($"Bank {item.OrganisationName} information updated with success");
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
