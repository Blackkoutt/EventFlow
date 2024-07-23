using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IFestival_MediaPatronRepository : IGenericRepository<Festival_MediaPatron>
    {
        Task<Festival_MediaPatron> GetOne(int festiwalId, int mediaPatronId);
        Task Delete(int festiwalId, int mediaPatronId);
    }
}
