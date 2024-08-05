using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class Festival_EventRepository(APIContext context) : GenericRepository<Festival_Event>(context), IFestival_EventRepository
    {
        public override sealed async Task<IEnumerable<Festival_Event>> GetAllAsync()
        {
            var records = await _context.Festival_Event
                                .Include(fe => fe.Festival)
                                .Include(fe => fe.Event)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public async Task<Festival_Event> GetOneAsync(int festiwalId, int eventId)
        {
            if (festiwalId <= 0 || eventId <= 0)
            {
                throw new ArgumentNullException(nameof(eventId), nameof(festiwalId));
            }
            var record = await _context.Festival_Event
                                .AsSplitQuery()
                                .Include(fe => fe.Festival)
                                .Include(fe => fe.Event)
                                .FirstOrDefaultAsync(fe => (fe.FestivalId == festiwalId && fe.EventId == eventId));

            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            return record;
        }
        public async Task DeleteAsync(int festiwalId, int eventId)
        {
            if (festiwalId <= 0 || eventId <= 0)
            {
                throw new ArgumentNullException(nameof(festiwalId), nameof(eventId));
            }

            var record = await _context.Festival_Event.FirstOrDefaultAsync(fe => (fe.FestivalId == festiwalId && fe.EventId == eventId));

            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            _context.Festival_Event.Remove(record);

            //await _context.SaveChangesAsync();
        }
    }
}
