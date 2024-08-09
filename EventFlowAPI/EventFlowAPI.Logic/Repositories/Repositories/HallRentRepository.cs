using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class HallRentRespository(APIContext context) : GenericRepository<HallRent>(context), IHallRentRepository
    {
        public override sealed async Task<IEnumerable<HallRent>> GetAllAsync()
        {
            return await _context.HallRent
                        .Include(hr => hr.User)
                        .Include(hr => hr.PaymentType)
                        .Include(hr => hr.Hall)
                        .Include(hr => hr.AdditionalServices)
                        .ThenInclude(hras => hras.AdditionalService)
                        .AsSplitQuery()
                        .ToListAsync();
        }
        public override sealed async Task<HallRent?> GetOneAsync(int id)
        {
            return await _context.HallRent
                        .AsSplitQuery()
                        .Include(hr => hr.User)
                        .Include(hr => hr.PaymentType)
                        .Include(hr => hr.Hall)
                        .Include(hr => hr.AdditionalServices)
                        .ThenInclude(hras => hras.AdditionalService)
                        .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
