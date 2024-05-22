using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using OpenBanking.Infra.Context;

namespace OpenBanking.Tests.Helpers
{
    public class DbFixture : IDisposable
    {
        private string _connectionString;
        private string _databaseName;
        private MongoClient _client;

        protected OpenBankingDbContext _context { get; }

        public DbFixture()
        {
            // Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString = config["OpenBankingStoreDatabase:ConnectionString"];
            _databaseName = $"test_db_{Guid.NewGuid()}";

            _client = new MongoClient(_connectionString);
            var dataBase = _client.GetDatabase(_databaseName);

            var optionsBuilder = new DbContextOptionsBuilder<OpenBankingDbContext>();
            optionsBuilder.UseMongoDB(dataBase.Client, dataBase.DatabaseNamespace.DatabaseName);
            _context = new OpenBankingDbContext(optionsBuilder.Options);
        }

        public void Dispose()
        {
            _client.DropDatabase(_databaseName);
        }
    }
}
