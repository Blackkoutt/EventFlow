using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface ITicketPDFRepository : IGenericRepository<TicketPDF>
    {
        Task<IEnumerable<TicketPDF>> GetAllPDFsForReservation(int reservationId);
    }
}
