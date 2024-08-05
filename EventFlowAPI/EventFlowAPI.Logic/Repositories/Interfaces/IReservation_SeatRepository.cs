using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IReservation_SeatRepository : IGenericRepository<Reservation_Seat>
    {
        Task DeleteAsync(int reservationId, int seatId);
        Task<Reservation_Seat> GetOneAsync(int reservationId, int seatId);
    }
}
