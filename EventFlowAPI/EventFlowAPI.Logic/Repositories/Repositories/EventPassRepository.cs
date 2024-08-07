using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class EventPassRepository(APIContext context) : GenericRepository<EventPass>(context), IEventPassRepository
    {
        public override sealed async Task<IEnumerable<EventPass>> GetAllAsync()
        {
            var records = await _context.EventPass
                                .Include(ep => ep.User)
                                .Include(ep => ep.PassType)
                                .Include(ep => ep.PaymentType)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public override sealed async Task<EventPass> GetOneAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 0, nameof(id));

            var record = await _context.EventPass
                                .AsSplitQuery()
                                .Include(ep => ep.User)
                                .Include(ep => ep.PassType)
                                .Include(ep => ep.PaymentType)
                                .FirstOrDefaultAsync(ep => ep.Id == id);

            return record ?? throw new KeyNotFoundException($"Entity with id {id} does not exist in database."); ;
        }
    }
}
