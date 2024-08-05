using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class EventPassRepository(APIContext context) : GenericRepository<EventPass>(context), IEventPassRepository
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
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var record = await _context.EventPass
                                .AsSplitQuery()
                                .Include(ep => ep.User)
                                .Include(ep => ep.PassType)
                                .Include(ep => ep.PaymentType)
                                .FirstOrDefaultAsync(ep => ep.Id == id);


            ArgumentNullException.ThrowIfNull(record, nameof(id));

            return record;
        }
    }
}
