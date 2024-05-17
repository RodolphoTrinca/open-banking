using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using OpenBanking.Application.Entity;

namespace OpenBanking.Infra.Context
{
    public class OpenBankingDbContext : DbContext
    {
        public DbSet<BankData> BankData { get; init; }

        public OpenBankingDbContext(DbContextOptions<OpenBankingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BankData>().ToCollection("banksData");
        }
    }
}
