using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class Festival_SponsorRepository(APIContext context) : GenericRepository<Festival_Sponsor>(context), IFestival_SponsorRepository
    {
        public override sealed async Task<IEnumerable<Festival_Sponsor>> GetAllAsync()
        {
            var records = await _context.Festival_Sponsor
                                .Include(fs => fs.Festival)
                                .Include(fs => fs.Sponsor)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public async Task<Festival_Sponsor> GetOneAsync(int festiwalId, int sponsorId)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(festiwalId, 0, nameof(festiwalId));
            ArgumentOutOfRangeException.ThrowIfLessThan(sponsorId, 0, nameof(sponsorId));

            var record = await _context.Festival_Sponsor
                                .AsSplitQuery()
                                .Include(fs => fs.Festival)
                                .Include(fs => fs.Sponsor)
                                .FirstOrDefaultAsync(fs => (fs.FestivalId == festiwalId && fs.SponsorId == sponsorId));

            return record ?? throw new KeyNotFoundException($"Entity with festiwalId {festiwalId} and sponsorId {sponsorId} does not exist in database."); ;
        }
        public async Task DeleteAsync(int festiwalId, int sponsorId)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(festiwalId, 0, nameof(festiwalId));
            ArgumentOutOfRangeException.ThrowIfLessThan(sponsorId, 0, nameof(sponsorId));

            var record = await _context.Festival_Sponsor.FirstOrDefaultAsync(fs =>
                         (fs.FestivalId == festiwalId && fs.SponsorId == sponsorId)) ??
                         throw new KeyNotFoundException($"Entity with festiwalId {festiwalId} and sponsorId {sponsorId} does not exist in database.");

            _context.Festival_Sponsor.Remove(record);
        }
    }
}
