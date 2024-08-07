using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class Festival_MediaPatronRepository(APIContext context) : GenericRepository<Festival_MediaPatron>(context), IFestival_MediaPatronRepository
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
            ArgumentOutOfRangeException.ThrowIfLessThan(festiwalId, 0, nameof(festiwalId));
            ArgumentOutOfRangeException.ThrowIfLessThan(mediaPatronId, 0, nameof(mediaPatronId));

            var record = await _context.Festival_MediaPatron
                                .AsSplitQuery()
                                .Include(fmp => fmp.Festival)
                                .Include(fmp => fmp.MediaPatron)
                                .FirstOrDefaultAsync(fmp => (fmp.FestivalId == festiwalId && fmp.MediaPatronId == mediaPatronId));

            return record ?? throw new KeyNotFoundException($"Entity with festiwalId {festiwalId} and mediaPatronId {mediaPatronId} does not exist in database.");
        }
        public async Task DeleteAsync(int festiwalId, int mediaPatronId)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(festiwalId, 0, nameof(festiwalId));
            ArgumentOutOfRangeException.ThrowIfLessThan(mediaPatronId, 0, nameof(mediaPatronId));

            var record = await _context.Festival_MediaPatron.FirstOrDefaultAsync(fmp =>
                         (fmp.FestivalId == festiwalId && fmp.MediaPatronId == mediaPatronId)) ??
                         throw new KeyNotFoundException($"Entity with festiwalId {festiwalId} and mediaPatronId {mediaPatronId} does not exist in database.");

            _context.Festival_MediaPatron.Remove(record);
        }
    }
}
