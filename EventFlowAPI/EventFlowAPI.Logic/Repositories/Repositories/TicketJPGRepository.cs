using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class TicketJPGRepository(APIContext context) : GenericRepository<TicketJPG>(context), ITicketJPGRepository
    {
        public async Task<IEnumerable<TicketJPG>> GetAllJPGsForReservation(int reservationId) =>
            await base.GetAllAsync(q => q.Where(t => t.Reservations.Any(r => r.Id == reservationId)));
    }
}
