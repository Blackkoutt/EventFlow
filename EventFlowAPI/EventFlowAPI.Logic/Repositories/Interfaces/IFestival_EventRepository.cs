using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IFestival_EventRepository : IRepository<Festival_Event>
    {
        Task Delete(int festiwalId, int eventId);
        Task<Festival_Event> GetOne(int festiwalId, int eventId);
    }
}
