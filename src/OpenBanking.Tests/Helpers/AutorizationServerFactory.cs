using OpenBanking.Application.Entity;

namespace OpenBanking.Tests.Helpers
{
    public class AutorizationServerFactory
    {
        private string _configurationURL;
        private string _discoveryAuthorization;
        private string _logoURI;

        public AutorizationServerFactory WithConfigurationURL(string configurationURL)
        {
            _configurationURL = configurationURL;
            return this;
        }

        public AutorizationServerFactory WithDiscoveryAuthorization(string discoveryAuthorization)
        {
            _discoveryAuthorization = discoveryAuthorization;
            return this;
        }

        public AutorizationServerFactory WithLogoURI(string logoURI)
        {
            _logoURI = logoURI;
            return this;
        }

        public AutorizationServer Build()
        {
            return new AutorizationServer()
            {
                configurationURL = _configurationURL,
                DiscoveryAuthorization = _discoveryAuthorization,
                LogoURI = _logoURI
            };
        }
    }
}
