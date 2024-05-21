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

            var organizationIds = _service.GetAll()
                .ToDictionary(bd => bd.OrganizationId, bd => bd);

            foreach (var item in data)
            {
                if (!Guid.TryParse(item.OrganisationId, out var id))
                {
                    _logger.LogError($"The Organisation id: {item.OrganisationId} cannot be converted to GUID");
                    continue;
                }

                _logger.LogDebug($"Search for bank data: {id}");

                if (!organizationIds.TryGetValue(id, out var bankData))
                {
                    _logger.LogDebug($"Bank {item.OrganisationName} not found, saving data...");
                    _logger.LogDebug("Creating bank data");
                    bankData = item.ToBankData();

                    _logger.LogDebug($"Saving bank data: {bankData}");
                    _service.SaveOrUpdate(bankData);

                    _logger.LogInformation($"Bank {item.OrganisationName} saved with success!");
                    continue;
                }

                _logger.LogDebug($"Bank {item.OrganisationName} found, updating data...");
                bankData.UpdateData(item);
                _service.SaveOrUpdate(bankData);

                organizationIds.Remove(id);

                _logger.LogInformation($"Bank {item.OrganisationName} information updated with success");
            }

            RemoveParticipants(organizationIds.Keys);
        }

        private void RemoveParticipants(IEnumerable<Guid> ids)
        {
            if (!ids.Any())
            {
                return;
            }

            _logger.LogInformation("Removing organizations that no longer participate");
            _logger.LogDebug($"Removing {ids.Count()} organizations");
            foreach (var item in ids)
            {
                var bankData = _service.GetByOrganizationId(item);
                if (bankData == null)
                {
                    continue;
                }

                _service.Remove(bankData);
            }
        }
    }
}
