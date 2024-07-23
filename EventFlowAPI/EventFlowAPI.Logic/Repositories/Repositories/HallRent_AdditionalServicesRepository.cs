using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class HallRent_AdditionalServicesRepository(APIContext context) : GenericRepository<HallRent_AdditionalServices>(context), IHallRent_AdditionalServicesRepository
    {
        public override sealed async Task<IEnumerable<HallRent_AdditionalServices>> GetAll()
        {
            var records = await _context.HallRent_AdditionalServices
                                .Include(hras => hras.HallRent)
                                .Include(hras => hras.AdditionalService)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public async Task<HallRent_AdditionalServices> GetOne(int hallRentId, int additionalServicesId)
        {
            if (hallRentId <= 0 || additionalServicesId <= 0)
            {
                throw new ArgumentNullException(nameof(hallRentId), nameof(additionalServicesId));
            }
            var record = await _context.HallRent_AdditionalServices
                                .AsSplitQuery()
                                .Include(hras => hras.HallRent)
                                .Include(hras => hras.AdditionalService)
                                .FirstOrDefaultAsync(hras => (hras.HallRentId == hallRentId && hras.AdditionalServiceId == additionalServicesId));

            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            return record;
        }
        public async Task Delete(int hallRentId, int additionalServicesId)
        {
            if (hallRentId <= 0 || additionalServicesId <= 0)
            {
                throw new ArgumentNullException(nameof(hallRentId), nameof(additionalServicesId));
            }

            var record = await _context.HallRent_AdditionalServices.FirstOrDefaultAsync(hras => (hras.HallRentId == hallRentId && hras.AdditionalServiceId == additionalServicesId));

            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            _context.HallRent_AdditionalServices.Remove(record);

            //await _context.SaveChangesAsync();
        }
    }
}
