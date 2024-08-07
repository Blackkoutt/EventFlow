using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class SeatRepository(APIContext context) : GenericRepository<Seat>(context), ISeatRepository
    {
        public override sealed async Task<IEnumerable<Seat>> GetAllAsync()
        {
            var records = await _context.Seat
                                .Include(s => s.Hall)
                                .Include(s => s.SeatType)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public override sealed async Task<Seat> GetOneAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 0, nameof(id));

            var record = await _context.Seat
                                .AsSplitQuery()
                                .Include(s => s.Hall)
                                .Include(s => s.SeatType)
                                .FirstOrDefaultAsync(e => e.Id == id);

            return record ?? throw new KeyNotFoundException($"Entity with id {id} does not exist in database."); ;
        }
    }
}
