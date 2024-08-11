using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class HallTypeRepository(APIContext context) : GenericRepository<HallType>(context), IHallTypeRepository
    {
        public override sealed async Task<IEnumerable<HallType>> GetAllAsync(Func<IQueryable<HallType>, IQueryable<HallType>>? query = null)
        {
            var _table = _context.HallType
                        .Include(ht => ht.Equipments)
                        //.ThenInclude(hte => hte.Equipment)
                        .AsSplitQuery();

            return await (query != null ? query(_table).ToListAsync() : _table.ToListAsync());
        }

        public override sealed async Task<HallType?> GetOneAsync(int id)
        {
            return await _context.HallType
                        .Include(ht => ht.Equipments)
                        //.ThenInclude(hte => hte.Equipment)
                        .FirstOrDefaultAsync(ht => ht.Id == id);
        }
    }
}
