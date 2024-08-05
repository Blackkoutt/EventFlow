using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class Festival_OrganizerRepository(APIContext context) : GenericRepository<Festival_Organizer>(context), IFestival_OrganizerRepository
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
            if (festiwalId <= 0 || organizerId <= 0)
            {
                throw new ArgumentNullException(nameof(organizerId), nameof(festiwalId));
            }
            var record = await _context.Festival_Organizer
                                .AsSplitQuery()
                                .Include(fo => fo.Festival)
                                .Include(fo => fo.Organizer)
                                .FirstOrDefaultAsync(fo => (fo.FestivalId == festiwalId && fo.OrganizerId == organizerId));

            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            return record;
        }
        public async Task DeleteAsync(int festiwalId, int organizerId)
        {
            if (festiwalId <= 0 || organizerId <= 0)
            {
                throw new ArgumentNullException(nameof(festiwalId), nameof(organizerId));
            }

            var record = await _context.Festival_Organizer.FirstOrDefaultAsync(fo => (fo.FestivalId == festiwalId && fo.OrganizerId == organizerId));

            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            _context.Festival_Organizer.Remove(record);

           // await _context.SaveChangesAsync();
        }
    }
}
