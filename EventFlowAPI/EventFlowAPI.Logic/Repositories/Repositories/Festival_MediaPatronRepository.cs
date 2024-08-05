using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class Festival_MediaPatronRepository(APIContext context) : GenericRepository<Festival_MediaPatron>(context), IFestival_MediaPatronRepository
    {
        public override sealed async Task<IEnumerable<Festival_MediaPatron>> GetAllAsync()
        {
            var records = await _context.Festival_MediaPatron
                                .Include(fmp => fmp.Festival)
                                .Include(fmp => fmp.MediaPatron)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public async Task<Festival_MediaPatron> GetOneAsync(int festiwalId, int mediaPatronId)
        {
            if (festiwalId <= 0 || mediaPatronId <= 0)
            {
                throw new ArgumentNullException(nameof(mediaPatronId), nameof(festiwalId));
            }
            var record = await _context.Festival_MediaPatron
                                .AsSplitQuery()
                                .Include(fmp => fmp.Festival)
                                .Include(fmp => fmp.MediaPatron)
                                .FirstOrDefaultAsync(fmp => (fmp.FestivalId == festiwalId && fmp.MediaPatronId == mediaPatronId));

            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            return record;
        }
        public async Task DeleteAsync(int festiwalId, int mediaPatronId)
        {
            if (festiwalId <= 0 || mediaPatronId <= 0)
            {
                throw new ArgumentNullException(nameof(mediaPatronId), nameof(festiwalId));
            }

            var record = await _context.Festival_MediaPatron.FirstOrDefaultAsync(fmp => (fmp.FestivalId == festiwalId && fmp.MediaPatronId == mediaPatronId));

            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            _context.Festival_MediaPatron.Remove(record);

            //await _context.SaveChangesAsync();
        }
    }
}
