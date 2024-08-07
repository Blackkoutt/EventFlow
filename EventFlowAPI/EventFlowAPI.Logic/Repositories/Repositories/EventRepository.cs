using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class EventRepository(APIContext context) : GenericRepository<Event>(context), IEventRepository
    {
        public override sealed async Task<IEnumerable<Event>> GetAllAsync()
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
        public override sealed async Task<Event> GetOneAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 0, nameof(id));

            var record = await _context.Event
                                .AsSplitQuery()
                                .Include(e => e.Hall)
                                .Include(e => e.Category)
                                .Include(e => e.Details)
                                .Include(e => e.Tickets)
                                .FirstOrDefaultAsync(e=> e.Id == id);

            return record ?? throw new KeyNotFoundException($"Entity with id {id} does not exist in database.");
        }
    }
}
