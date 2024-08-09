using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class FestivalRepository(APIContext context) : GenericRepository<Festival>(context), IFestivalRepository
    {
        public override sealed async Task<IEnumerable<Festival>> GetAllAsync()
        {
            return await _context.Festival
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
        }
        public override sealed async Task<Festival?> GetOneAsync(int id)
        {
            return await _context.Festival
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
        }
    }
}
