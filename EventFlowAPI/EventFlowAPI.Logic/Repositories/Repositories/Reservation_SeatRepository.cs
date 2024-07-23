using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class Reservation_SeatRepository(APIContext context) : GenericRepository<Reservation_Seat>(context), IReservation_SeatRepository
    {
        public override sealed async Task<IEnumerable<Reservation_Seat>> GetAll()
        {
            var records = await _context.Reservation_Seat
                                .Include(rs => rs.Reservation)
                                .Include(rs => rs.Seat)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public async Task<Reservation_Seat> GetOne(int reservationId, int seatId)
        {
            if (reservationId <= 0 || seatId <= 0)
            {
                throw new ArgumentNullException(nameof(reservationId), nameof(seatId));
            }
            var record = await _context.Reservation_Seat
                                .AsSplitQuery()
                                .Include(rs => rs.Reservation)
                                .Include(rs => rs.Seat)
                                .FirstOrDefaultAsync(rs => (rs.ReservationId == reservationId && rs.SeatId == seatId));

            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            return record;
        }
        public async Task Delete(int reservationId, int seatId)
        {
            if (reservationId <= 0 || seatId <= 0)
            {
                throw new ArgumentNullException(nameof(reservationId), nameof(seatId));
            }

            var record = await _context.Reservation_Seat.FirstOrDefaultAsync(rs => (rs.ReservationId == reservationId && rs.SeatId == seatId));

            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            _context.Reservation_Seat.Remove(record);

            //await _context.SaveChangesAsync();
        }

    }
}
