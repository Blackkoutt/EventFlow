using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class HallType_EquipmentRepository(APIContext context) : GenericRepository<HallType_Equipment>(context), IHallType_EquipmentRepository
    {
        public override sealed async Task<IEnumerable<HallType_Equipment>> GetAllAsync()
        {
            var records = await _context.HallType_Equipment
                                .Include(hte => hte.HallType)
                                .Include(hte => hte.Equipment)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public async Task<HallType_Equipment> GetOneAsync(int typeId, int equipmentId)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(typeId, 0, nameof(typeId));
            ArgumentOutOfRangeException.ThrowIfLessThan(equipmentId, 0, nameof(equipmentId));

            var record = await _context.HallType_Equipment
                                .AsSplitQuery()
                                .Include(hte => hte.HallType)
                                .Include(hte => hte.Equipment)
                                .FirstOrDefaultAsync(hte => (hte.HallTypeId == typeId && hte.EquipmentId == equipmentId));

            return record ?? throw new KeyNotFoundException($"Entity with typeId {typeId} and equipmentId {equipmentId} does not exist in database."); ;
        }
        public async Task DeleteAsync(int typeId, int equipmentId)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(typeId, 0, nameof(typeId));
            ArgumentOutOfRangeException.ThrowIfLessThan(equipmentId, 0, nameof(equipmentId));

            var record = await _context.HallType_Equipment.FirstOrDefaultAsync(hte =>
                        (hte.HallTypeId == typeId && hte.EquipmentId == equipmentId)) ??
                        throw new KeyNotFoundException($"Entity with typeId {typeId} and equipmentId {equipmentId} does not exist in database.");

            _context.HallType_Equipment.Remove(record);
        }
    }
}
