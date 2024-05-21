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

        public BankData? GetOrganizationById(Guid id)
        {
            return _context.BankData.FirstOrDefault(k => k.OrganizationId == id);
        }

        public BankData? GetById(ObjectId id)
        {
            return _context.BankData.FirstOrDefault(k => k.Id == id);
        }

        public IEnumerable<BankData> GetAll(int? skip = null, int? take = null)
        {
            var query = from bankData in _context.BankData
                        select bankData;

            if (skip != null)
            {
                query.Skip(skip.Value);
            }

            if (take != null)
            {
                query.Take(take.Value);
            }

            return query.ToList();
        }

        public IEnumerable<Guid> GetAllOrganizationIds()
        {
            return _context.BankData
                .Select(k => k.OrganizationId)
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
            if (GetById(obj.Id) == null)
            {
                _context.BankData.Add(obj);
            }

            _context.ChangeTracker.DetectChanges();
            _logger.LogDebug(_context.ChangeTracker.DebugView.LongView);
            _context.SaveChanges();
        }
    }
}
