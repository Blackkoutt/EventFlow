using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class HallType_EquipmentRepository(APIContext context) : GenericRepository<HallType_Equipment>(context), IHallType_EquipmentRepository
    {
        public override sealed async Task<IEnumerable<HallType_Equipment>> GetAll()
        {
            var records = await _context.HallType_Equipment
                                .Include(hte => hte.HallType)
                                .Include(hte => hte.Equipment)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public async Task<HallType_Equipment> GetOne(int typeId, int equipmentId)
        {
            if (typeId <= 0 || equipmentId <= 0)
            {
                throw new ArgumentNullException(nameof(typeId), nameof(equipmentId));
            }
            var record = await _context.HallType_Equipment
                                .AsSplitQuery()
                                .Include(hte => hte.HallType)
                                .Include(hte => hte.Equipment)
                                .FirstOrDefaultAsync(hte => (hte.HallTypeId == typeId && hte.EquipmentId == equipmentId));

            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            return record;
        }
        public async Task Delete(int typeId, int equipmentId)
        {
            if (typeId <= 0 || equipmentId <= 0)
            {
                throw new ArgumentNullException(nameof(typeId), nameof(equipmentId));
            }

            var record = await _context.HallType_Equipment.FirstOrDefaultAsync(hte => (hte.HallTypeId == typeId && hte.EquipmentId == equipmentId));

            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            _context.HallType_Equipment.Remove(record);

           // await _context.SaveChangesAsync();
        }
    }
}
