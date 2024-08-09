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
            return await _context.Seat
                                .Include(s => s.Hall)
                                .Include(s => s.SeatType)
                                .AsSplitQuery()
                                .ToListAsync();
        }
        public override sealed async Task<Seat?> GetOneAsync(int id)
        {
            return await _context.Seat
                        .AsSplitQuery()
                        .Include(s => s.Hall)
                        .Include(s => s.SeatType)
                        .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
