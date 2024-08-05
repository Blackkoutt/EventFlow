using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IFestival_EventRepository : IGenericRepository<Festival_Event>
    {
        Task DeleteAsync(int festiwalId, int eventId);
        Task<Festival_Event> GetOneAsync(int festiwalId, int eventId);
    }
}
