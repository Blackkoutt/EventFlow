using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IFestival_SponsorRepository : IGenericRepository<Festival_Sponsor>
    {
        Task DeleteAsync(int festiwalId, int sponsorId);
        Task<Festival_Sponsor> GetOneAsync(int festiwalId, int sponsorId);
    }
}
