using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IHallType_EquipmentRepository : IRepository<HallType_Equipment>
    {
        Task<HallType_Equipment> GetOne(int typeId, int equipmentId);
        Task Delete(int typeId, int equipmentId);
    }
}
