using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class ReservationRepository(APIContext context) : Repository<Reservation>(context), IReservationRepository
    {
        public override sealed async Task<IEnumerable<Reservation>> GetAll()
        {
            var records = await _context.Reservation
                                .Include(r => r.User)
                                .Include(r => r.PaymentType)
                                .Include(r => r.Seats)
                                .ThenInclude(rs => rs.Seat)
                                .Include(r => r.Ticket)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public override sealed async Task<Reservation> GetOne(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var record = await _context.Reservation
                                .AsSplitQuery()
                                .Include(r => r.User)
                                .Include(r => r.PaymentType)
                                .Include(r => r.Seats)
                                .ThenInclude(rs => rs.Seat)
                                .Include(r => r.Ticket)
                                .FirstOrDefaultAsync(e => e.Id == id);


            ArgumentNullException.ThrowIfNull(record, nameof(id));

            return record;
        }
    }
}
