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
            var records = await _context.Hall
                                .Include(h => h.Seats)
                                .Include(h => h.Type)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public override sealed async Task<Hall> GetOneAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 0, nameof(id));

            var record = await _context.Hall
                                .AsSplitQuery()
                                .Include(h => h.Seats)
                                .Include(h => h.Type)
                                .FirstOrDefaultAsync(h => h.Id == id);

            return record ?? throw new KeyNotFoundException($"Entity with id {id} does not exist in database."); ;
        }
    }
}
