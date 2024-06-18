using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class FestivalRepository(APIContext context) : Repository<Festival>(context), IFestivalRepository
    {
        public override sealed async Task<IEnumerable<Festival>> GetAll()
        {
            var records = await _context.Festival
                                .Include(f => f.Details)
                                .Include(f => f.Events)
                                .ThenInclude(fe => fe.Event)
                                .Include(f => f.Sponsors)
                                .ThenInclude(fs => fs.Sponsor)
                                .Include(f => f.MediaPatrons)
                                .ThenInclude(fmp => fmp.MediaPatron)
                                .Include(f => f.Organizers)
                                .ThenInclude(fo => fo.Organizer)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public override sealed async Task<Festival> GetOne(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var record = await _context.Festival
                                .AsSplitQuery()
                                .Include(f => f.Details)
                                .Include(f => f.Events)
                                .ThenInclude(fe => fe.Event)
                                .Include(f => f.Sponsors)
                                .ThenInclude(fs => fs.Sponsor)
                                .Include(f => f.MediaPatrons)
                                .ThenInclude(fmp => fmp.MediaPatron)
                                .Include(f => f.Organizers)
                                .ThenInclude(fo => fo.Organizer)
                                .FirstOrDefaultAsync(f => f.Id == id);


            ArgumentNullException.ThrowIfNull(record, nameof(id));

            return record;
        }
    }
}
