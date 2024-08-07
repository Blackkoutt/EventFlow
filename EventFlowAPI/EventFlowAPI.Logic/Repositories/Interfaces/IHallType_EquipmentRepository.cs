﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Repositories.Interfaces
{
    public interface IHallType_EquipmentRepository : IGenericRepository<HallType_Equipment>
    {
        Task<HallType_Equipment> GetOne(int typeId, int equipmentId);
        Task Delete(int typeId, int equipmentId);
    }
}
