using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class HallTypeRepository(APIContext context) : GenericRepository<HallType>(context), IHallTypeRepository
    {
        public override sealed async Task<IEnumerable<HallType>> GetAllAsync()
        {
            var records = await _context.HallType
                                .Include(ht => ht.Equipments)
                                .ThenInclude(hte => hte.Equipment)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public override sealed async Task<HallType> GetOneAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var record = await _context.HallType
                                .Include(ht => ht.Equipments)
                                .ThenInclude(hte => hte.Equipment)
                                .FirstOrDefaultAsync(ht => ht.Id == id);


            ArgumentNullException.ThrowIfNull(record, nameof(id));

            return record;
        }
    }
}
