using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class Reservation_SeatRepository(APIContext context) : GenericRepository<Reservation_Seat>(context), IReservation_SeatRepository
    {
        public override sealed async Task<IEnumerable<Reservation_Seat>> GetAllAsync()
        {
            var records = await _context.Reservation_Seat
                                .Include(rs => rs.Reservation)
                                .Include(rs => rs.Seat)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public async Task<Reservation_Seat> GetOneAsync(int reservationId, int seatId)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(reservationId, 0, nameof(reservationId));
            ArgumentOutOfRangeException.ThrowIfLessThan(seatId, 0, nameof(seatId));

            var record = await _context.Reservation_Seat
                                .AsSplitQuery()
                                .Include(rs => rs.Reservation)
                                .Include(rs => rs.Seat)
                                .FirstOrDefaultAsync(rs => (rs.ReservationId == reservationId && rs.SeatId == seatId));

            return record ?? throw new KeyNotFoundException($"Entity with reservationId {reservationId} and seatId {seatId} does not exist in database."); ;
        }
        public async Task DeleteAsync(int reservationId, int seatId)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(reservationId, 0, nameof(reservationId));
            ArgumentOutOfRangeException.ThrowIfLessThan(seatId, 0, nameof(seatId));

            var record = await _context.Reservation_Seat.FirstOrDefaultAsync(rs =>
                         (rs.ReservationId == reservationId && rs.SeatId == seatId)) ??
                         throw new KeyNotFoundException($"Entity with reservationId {reservationId} and seatId {seatId} does not exist in database.");

            _context.Reservation_Seat.Remove(record);
        }

    }
}
