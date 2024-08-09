using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class ReservationRepository(APIContext context) : GenericRepository<Reservation>(context), IReservationRepository
    {
        public override sealed async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservation
                                .Include(r => r.User)
                                .Include(r => r.PaymentType)
                                .Include(r => r.Seats)
                                .ThenInclude(rs => rs.Seat)
                                .Include(r => r.Ticket)
                                .AsSplitQuery()
                                .ToListAsync();
        }
        public override sealed async Task<Reservation?> GetOneAsync(int id)
        {
            return await _context.Reservation
                        .AsSplitQuery()
                        .Include(r => r.User)
                        .Include(r => r.PaymentType)
                        .Include(r => r.Seats)
                        .ThenInclude(rs => rs.Seat)
                        .Include(r => r.Ticket)
                        .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
