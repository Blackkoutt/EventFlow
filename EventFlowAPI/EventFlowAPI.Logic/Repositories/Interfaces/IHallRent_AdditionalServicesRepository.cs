using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IHallRent_AdditionalServicesRepository : IRepository<HallRent_AdditionalServices>
    {
        Task<HallRent_AdditionalServices> GetOne(int hallRentId, int additionalServicesId);
        Task Delete(int hallRentId, int additionalServicesId);
    }
}
