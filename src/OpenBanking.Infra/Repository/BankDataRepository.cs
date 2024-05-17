using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using OpenBanking.Application.Entity;
using OpenBanking.Application.Interfaces;
using OpenBanking.Infra.Context;

namespace OpenBanking.Infra.Repository
{
    public class BankDataRepository : IBankDataRepository
    {
        private readonly OpenBankingDbContext _context;
        private readonly ILogger<BankDataRepository> _logger;

        public BankDataRepository(OpenBankingDbContext context, ILogger<BankDataRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public BankData? GetById(ObjectId id)
        {
            return _context.BankData.FirstOrDefault(k => k.Id == id);
        }

        public IEnumerable<BankData> GetAll(int skip = 0, int take = 10)
        {
            return _context.BankData
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public void Remove(BankData obj)
        {
            _context.BankData.Remove(obj);

            _context.ChangeTracker.DetectChanges();
            _logger.LogDebug(_context.ChangeTracker.DebugView.LongView);

            _context.SaveChanges();
        }

        public void SaveOrUpdate(BankData obj)
        {
            if (obj.Id == ObjectId.Empty)
            {
                _context.BankData.Add(obj);
            }

            _context.ChangeTracker.DetectChanges();
            _logger.LogDebug(_context.ChangeTracker.DebugView.LongView);
            _context.SaveChanges();
        }
    }
}
