using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class HallRentRepository(APIContext context) : Repository<HallRent>(context), IHallRentRepository
    {
        public override sealed async Task<IEnumerable<HallRent>> GetAll()
        {
            var records = await _context.HallRent
                                .Include(hr => hr.User)
                                .Include(hr => hr.PaymentType)
                                .Include(hr => hr.Hall)
                                .Include(hr => hr.AdditionalServices)
                                .ThenInclude(hras => hras.AdditionalService)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public override sealed async Task<HallRent> GetOne(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var record = await _context.HallRent
                                .AsSplitQuery()
                                .Include(hr => hr.User)
                                .Include(hr => hr.PaymentType)
                                .Include(hr => hr.Hall)
                                .Include(hr => hr.AdditionalServices)
                                .ThenInclude(hras => hras.AdditionalService)
                                .FirstOrDefaultAsync(e => e.Id == id);


            ArgumentNullException.ThrowIfNull(record, nameof(id));

            return record;
        }
    }
}
