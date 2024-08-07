using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class Festival_OrganizerRepository(APIContext context) : GenericRepository<Festival_Organizer>(context), IFestival_OrganizerRepository
    {
        public override sealed async Task<IEnumerable<Festival_Organizer>> GetAllAsync()
        {
            var records = await _context.Festival_Organizer
                                .Include(fo => fo.Festival)
                                .Include(fo => fo.Organizer)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public async Task<Festival_Organizer> GetOneAsync(int festiwalId, int organizerId)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(festiwalId, 0, nameof(festiwalId));
            ArgumentOutOfRangeException.ThrowIfLessThan(organizerId, 0, nameof(organizerId));

            var record = await _context.Festival_Organizer
                                .AsSplitQuery()
                                .Include(fo => fo.Festival)
                                .Include(fo => fo.Organizer)
                                .FirstOrDefaultAsync(fo => (fo.FestivalId == festiwalId && fo.OrganizerId == organizerId));

            return record ?? throw new KeyNotFoundException($"Entity with festiwalId {festiwalId} and organizerId {organizerId} does not exist in database."); ;
        }
        public async Task DeleteAsync(int festiwalId, int organizerId)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(festiwalId, 0, nameof(festiwalId));
            ArgumentOutOfRangeException.ThrowIfLessThan(organizerId, 0, nameof(organizerId));

            var record = await _context.Festival_Organizer.FirstOrDefaultAsync(fo => 
                         (fo.FestivalId == festiwalId && fo.OrganizerId == organizerId)) ??
                         throw new KeyNotFoundException($"Entity with festiwalId {festiwalId} and organizerId {organizerId} does not exist in database.");

            _context.Festival_Organizer.Remove(record);
        }
    }
}
