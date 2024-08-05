using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IFestival_OrganizerRepository : IGenericRepository<Festival_Organizer>
    {
        Task<Festival_Organizer> GetOneAsync(int festiwalId, int organizerId);
        Task DeleteAsync(int festiwalId, int organizerId);
    }
}
