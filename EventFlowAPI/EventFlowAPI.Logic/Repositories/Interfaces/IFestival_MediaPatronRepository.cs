using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IFestival_MediaPatronRepository : IGenericRepository<Festival_MediaPatron>
    {
        Task<Festival_MediaPatron> GetOneAsync(int festiwalId, int mediaPatronId);
        Task DeleteAsync(int festiwalId, int mediaPatronId);
    }
}
