using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class HallRepository(APIContext context) : GenericRepository<Hall>(context), IHallRepository
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
        public override sealed async Task<Hall> GetOneAsync(int hallNr)
        {
            if (hallNr <= 0)
            {
                throw new ArgumentNullException(nameof(hallNr));
            }
            var record = await _context.Hall
                                .AsSplitQuery()
                                .Include(h => h.Seats)
                                .Include(h => h.Type)
                                .FirstOrDefaultAsync(h => h.HallNr == hallNr);


            ArgumentNullException.ThrowIfNull(record, nameof(hallNr));

            return record;
        }
    }
}
