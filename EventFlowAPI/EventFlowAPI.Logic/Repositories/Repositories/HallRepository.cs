using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class HallRepository(APIContext context) : GenericRepository<Hall>(context), IHallRepository
    {
        public override sealed async Task<IEnumerable<Hall>> GetAllAsync()
        {
            return await _context.Hall
                        .Include(h => h.Seats)
                        .Include(h => h.Type)
                        .AsSplitQuery()
                        .ToListAsync();
        }
        public override sealed async Task<Hall?> GetOneAsync(int id)
        {
            return await _context.Hall
                        .AsSplitQuery()
                        .Include(h => h.Seats)
                        .Include(h => h.Type)
                        .FirstOrDefaultAsync(h => h.Id == id);
        }
    }
}
