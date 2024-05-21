using OpenBanking.Application.Entity;

namespace OpenBanking.Worker.DTO
{
    [Serializable]
    public class AuthorisationServer
    {
        public string AuthorisationServerId { get; set; }
        public string CustomerFriendlyLogoUri { get; set; }
        public string OpenIDDiscoveryDocument { get; set; }

        public AutorizationServer ToAutorizationServer()
        {
            return new AutorizationServer
            {
                configurationURL = OpenIDDiscoveryDocument,
                DiscoveryAuthorization = AuthorisationServerId,
                LogoURI = CustomerFriendlyLogoUri
            };
        }
    }
}
