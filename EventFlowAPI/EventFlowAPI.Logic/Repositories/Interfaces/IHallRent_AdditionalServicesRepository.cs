using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IHallRent_AdditionalServicesRepository : IGenericRepository<HallRent_AdditionalServices>
    {
        Task<HallRent_AdditionalServices> GetOneAsync(int hallRentId, int additionalServicesId);
        Task DeleteAsync(int hallRentId, int additionalServicesId);
    }
}
