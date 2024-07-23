using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class EventRepository(APIContext context) : GenericRepository<Event>(context), IEventRepository
    {
        public override sealed async Task<IEnumerable<Event>> GetAll()
        {
            var records = await _context.Event
                                .Include(e => e.Hall)
                                .Include(e => e.Category)
                                .Include(e => e.Details)
                                .Include(e => e.Tickets)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public override sealed async Task<Event> GetOne(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var record = await _context.Event
                                .AsSplitQuery()
                                .Include(e => e.Hall)
                                .Include(e => e.Category)
                                .Include(e => e.Details)
                                .Include(e => e.Tickets)
                                .FirstOrDefaultAsync(e=> e.Id == id);


            ArgumentNullException.ThrowIfNull(record, nameof(id));

            return record;
        }
    }
}
