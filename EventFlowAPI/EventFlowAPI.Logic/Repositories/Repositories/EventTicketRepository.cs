using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class EventTicketRepository(APIContext context) : GenericRepository<EventTicket>(context), IEventTicketRepository
    {
        public override sealed async Task<IEnumerable<EventTicket>> GetAllAsync()
        {
            return await _context.EventTicket
                        .Include(et => et.TicketType)
                        .Include(et => et.Event)
                        .AsSplitQuery()
                        .ToListAsync();
        }
        public override sealed async Task<EventTicket?> GetOneAsync(int id)
        {
            return await _context.EventTicket
                        .AsSplitQuery()
                        .Include(et => et.TicketType)
                        .Include(et => et.Event)
                        .FirstOrDefaultAsync(et => et.Id == id);
        }
    }
}
