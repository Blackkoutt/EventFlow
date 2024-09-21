using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface ITicketJPGRepository : IGenericRepository<TicketJPG>
    {
        Task<IEnumerable<TicketJPG>> GetAllJPGsForReservation(int reservationId);
    }
}
