using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class HallTypeRepository(APIContext context) : GenericRepository<HallType>(context), IHallTypeRepository
    {
        public sealed override async Task<HallType?> GetOneAsync(int id)
        {
            return await _context.HallType
                        .Include(ht => ht.Equipments)
                        .FirstOrDefaultAsync(ht => ht.Id == id);
        }
    }
}
