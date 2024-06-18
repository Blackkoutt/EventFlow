using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IReservation_SeatRepository : IRepository<Reservation_Seat>
    {
        Task Delete(int reservationId, int seatId);
        Task<Reservation_Seat> GetOne(int reservationId, int seatId);
    }
}
