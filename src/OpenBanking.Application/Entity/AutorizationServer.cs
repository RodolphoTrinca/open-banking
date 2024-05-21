
namespace OpenBanking.Application.Entity
{
    public class AutorizationServer : IEquatable<AutorizationServer?>
    {
        public string? LogoURI { get; set; }
        public string? ConfigurarionURL { get; set; }
        public string? DiscoveryAuthorization { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as AutorizationServer);
        }

        public bool Equals(AutorizationServer? other)
        {
            return other is not null &&
                   LogoURI == other.LogoURI &&
                   ConfigurarionURL == other.ConfigurarionURL &&
                   DiscoveryAuthorization == other.DiscoveryAuthorization;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LogoURI, ConfigurarionURL, DiscoveryAuthorization);
        }

        public static bool operator ==(AutorizationServer? left, AutorizationServer? right)
        {
            return EqualityComparer<AutorizationServer>.Default.Equals(left, right);
        }

        public static bool operator !=(AutorizationServer? left, AutorizationServer? right)
        {
            return !(left == right);
        }
    }
}
