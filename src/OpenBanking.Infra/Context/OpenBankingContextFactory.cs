using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBanking.Infra.Context
{
    public class OpenBankingContextFactory : IDesignTimeDbContextFactory<OpenBankingDbContext>
    {
        public OpenBankingDbContext CreateDbContext(string[] args)
        {
            // Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../OpenBanking.API"))
                .AddJsonFile("appsettings.local.json")
                .Build();

            var client = new MongoClient(config["OpenBankingStoreDatabase:ConnectionString"]);
            var dataBase = client.GetDatabase(config["OpenBankingStoreDatabase:DatabaseName"]);

            var optionsBuilder = new DbContextOptionsBuilder<OpenBankingDbContext>();
            optionsBuilder.UseMongoDB(dataBase.Client, dataBase.DatabaseNamespace.DatabaseName);
            return new OpenBankingDbContext(optionsBuilder.Options);
        }
    }
}
