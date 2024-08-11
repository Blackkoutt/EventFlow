using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class ReservationRepository(APIContext context) : GenericRepository<Reservation>(context), IReservationRepository
    {
        public override sealed async Task<IEnumerable<Reservation>> GetAllAsync(Func<IQueryable<Reservation>, IQueryable<Reservation>>? query = null)
        {
            var _table = _context.Reservation
                                .Include(r => r.User)
                                .Include(r => r.PaymentType)
                                .Include(r => r.Seats)
                                .Include(r => r.Ticket)
                                .AsSplitQuery();

            return await (query != null ? query(_table).ToListAsync() : _table.ToListAsync());
        }

        public override sealed async Task<Reservation?> GetOneAsync(int id)
        {
            return await _context.Reservation
                        .AsSplitQuery()
                        .Include(r => r.User)
                        .Include(r => r.PaymentType)
                        .Include(r => r.Seats)
                        .Include(r => r.Ticket)
                        .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
