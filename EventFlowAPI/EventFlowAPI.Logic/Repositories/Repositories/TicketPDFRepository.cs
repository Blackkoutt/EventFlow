using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class TicketPDFRepository(APIContext context) : GenericRepository<TicketPDF>(context), ITicketPDFRepository
    {
        public async Task<IEnumerable<TicketPDF>> GetAllPDFsForReservation(int reservationId) =>
            await base.GetAllAsync(q => q.Where(t => t.Reservations.Any(r => r.Id == reservationId)));
    }
}
