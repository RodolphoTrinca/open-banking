using MongoDB.Bson;

namespace OpenBanking.Application.Entity
{
    public class BankData
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string LogoURI { get; set; }
        public string ConfigurarionURL { get; set;}
        public string DiscoveryAuthorization { get; set; }
    }
}
