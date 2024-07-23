using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IReservation_SeatRepository : IGenericRepository<Reservation_Seat>
    {
        Task Delete(int reservationId, int seatId);
        Task<Reservation_Seat> GetOne(int reservationId, int seatId);
    }
}
