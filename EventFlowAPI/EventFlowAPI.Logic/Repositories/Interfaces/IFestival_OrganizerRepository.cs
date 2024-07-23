using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IFestival_OrganizerRepository : IGenericRepository<Festival_Organizer>
    {
        Task<Festival_Organizer> GetOne(int festiwalId, int organizerId);
        Task Delete(int festiwalId, int organizerId);
    }
}
