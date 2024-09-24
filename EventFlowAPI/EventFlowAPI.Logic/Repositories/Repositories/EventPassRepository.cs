using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class EventPassRepository(APIContext context) : GenericRepository<EventPass>(context), IEventPassRepository
    {
        public sealed override async Task<IEnumerable<EventPass>> GetAllAsync(Func<IQueryable<EventPass>, IQueryable<EventPass>>? query = null)
        {
            var _table = _context.EventPass
                    .Include(ep => ep.User)
                    .Include(ep => ep.PassType)
                    .Include(ep => ep.PaymentType)
                    .AsSplitQuery();

            return await (query != null ? query(_table).ToListAsync() : _table.ToListAsync());
        }
        public sealed override async Task<EventPass?> GetOneAsync(int id)
        {
            return await _context.EventPass
                        .AsSplitQuery()
                        .Include(ep => ep.User)
                        .Include(ep => ep.PassType)
                        .Include(ep => ep.PaymentType)
                        .FirstOrDefaultAsync(ep => ep.Id == id);
        }
    }
}
