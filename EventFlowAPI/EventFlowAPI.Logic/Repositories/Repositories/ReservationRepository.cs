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
        public override sealed async Task<Reservation> GetOneAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 0, nameof(id));

            var record = await _context.Reservation
                                .AsSplitQuery()
                                .Include(r => r.User)
                                .Include(r => r.PaymentType)
                                .Include(r => r.Seats)
                                .ThenInclude(rs => rs.Seat)
                                .Include(r => r.Ticket)
                                .FirstOrDefaultAsync(e => e.Id == id);

            return record ?? throw new KeyNotFoundException($"Entity with id {id} does not exist in database."); ;
        }
    }
}
