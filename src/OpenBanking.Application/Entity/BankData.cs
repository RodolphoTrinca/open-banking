using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OpenBanking.Application.Entity
{
    public class BankData : IEquatable<BankData?>
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string? Status { get; set; }
        public string? Name { get; set; }
        public List<AutorizationServer> AutorizationServers { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as BankData);
        }

        internal BankData Update(BankData obj)
        {
            if (obj == null)
            {
                return this;
            }

            Id = obj.Id;
            OrganizationId = obj.OrganizationId;
            Name = obj.Name;
            Status = obj.Status;
            AutorizationServers = obj.AutorizationServers;

            return this;
        }

        public bool Equals(BankData? other)
        {
            return other is not null &&
                   Id.Equals(other.Id) &&
                   Status == other.Status &&
                   Name == other.Name &&
                   OrganizationId.Equals(other.OrganizationId) &&
                   EqualityComparer<List<AutorizationServer>>.Default.Equals(AutorizationServers, other.AutorizationServers);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Status, Name, AutorizationServers);
        }

        public static bool operator ==(BankData? left, BankData? right)
        {
            return EqualityComparer<BankData>.Default.Equals(left, right);
        }

        public static bool operator !=(BankData? left, BankData? right)
        {
            return !(left == right);
        }
    }
}
