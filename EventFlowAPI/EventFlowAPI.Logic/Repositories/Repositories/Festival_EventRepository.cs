using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class Festival_EventRepository(APIContext context) : GenericRepository<Festival_Event>(context), IFestival_EventRepository
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
            ArgumentOutOfRangeException.ThrowIfLessThan(festiwalId, 0, nameof(festiwalId));
            ArgumentOutOfRangeException.ThrowIfLessThan(eventId, 0, nameof(eventId));

            var record = await _context.Festival_Event
                                .AsSplitQuery()
                                .Include(fe => fe.Festival)
                                .Include(fe => fe.Event)
                                .FirstOrDefaultAsync(fe => (fe.FestivalId == festiwalId && fe.EventId == eventId));

            return record ?? throw new KeyNotFoundException($"Entity with festiwalId {festiwalId} and eventId {eventId} does not exist in database.");
        }
        public async Task DeleteAsync(int festiwalId, int eventId)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(festiwalId, 0, nameof(festiwalId));
            ArgumentOutOfRangeException.ThrowIfLessThan(eventId, 0, nameof(eventId));

            var record = await _context.Festival_Event.FirstOrDefaultAsync(fe =>
                         (fe.FestivalId == festiwalId && fe.EventId == eventId)) ??
                         throw new KeyNotFoundException($"Entity with festiwalId {festiwalId} and eventId {eventId} does not exist in database.");

            _context.Festival_Event.Remove(record);
        }
    }
}
