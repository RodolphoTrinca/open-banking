using OpenBanking.Application.Entity;
using ThirdParty.Json.LitJson;

namespace OpenBanking.Worker.DTO
{
    [Serializable]
    internal class CompanyData
    {
        public string OrganisationId { get; set; }
        public string Status { get; set; }
        public string OrganisationName { get; set; }

        public List<AuthorisationServer> AuthorisationServers { get; set; }

        public BankData ToBankData()
        {
            var listAutorizationServers = AuthorisationServers
                .Select(a => a.ToAutorizationServer())
                .ToList();

            return new BankData { 
                Id = Guid.Parse(OrganisationId),
                Status = Status,
                Name = OrganisationName,
                AutorizationServers = listAutorizationServers,
            };
        }
    } 
}
