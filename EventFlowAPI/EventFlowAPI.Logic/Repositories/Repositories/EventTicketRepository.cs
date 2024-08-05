using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class EventTicketRepository(APIContext context) : GenericRepository<EventTicket>(context), IEventTicketRepository
    {
        public override sealed async Task<IEnumerable<EventTicket>> GetAllAsync()
        {
            var records = await _context.EventTicket
                                .Include(et => et.TicketType)
                                .Include(et => et.Event)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public override sealed async Task<EventTicket> GetOneAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var record = await _context.EventTicket
                                .AsSplitQuery()
                                .Include(et => et.TicketType)
                                .Include(et => et.Event)
                                .FirstOrDefaultAsync(et => et.Id == id);


            ArgumentNullException.ThrowIfNull(record, nameof(id));

            return record;
        }
    }
}
