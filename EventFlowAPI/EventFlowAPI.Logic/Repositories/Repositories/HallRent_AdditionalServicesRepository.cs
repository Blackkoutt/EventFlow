using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class HallRent_AdditionalServicesRepository(APIContext context) : GenericRepository<HallRent_AdditionalServices>(context), IHallRent_AdditionalServicesRepository
    {
        public override sealed async Task<IEnumerable<HallRent_AdditionalServices>> GetAllAsync()
        {
            var records = await _context.HallRent_AdditionalServices
                                .Include(hras => hras.HallRent)
                                .Include(hras => hras.AdditionalService)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public async Task<HallRent_AdditionalServices> GetOneAsync(int hallRentId, int additionalServicesId)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(hallRentId, 0, nameof(hallRentId));
            ArgumentOutOfRangeException.ThrowIfLessThan(additionalServicesId, 0, nameof(additionalServicesId));

            var record = await _context.HallRent_AdditionalServices
                                .AsSplitQuery()
                                .Include(hras => hras.HallRent)
                                .Include(hras => hras.AdditionalService)
                                .FirstOrDefaultAsync(hras => (hras.HallRentId == hallRentId && hras.AdditionalServiceId == additionalServicesId));

            return record ?? throw new KeyNotFoundException($"Entity with hallRentId {hallRentId} and additionalServicesId {additionalServicesId} does not exist in database."); ;
        }
        public async Task DeleteAsync(int hallRentId, int additionalServicesId)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(hallRentId, 0, nameof(hallRentId));
            ArgumentOutOfRangeException.ThrowIfLessThan(additionalServicesId, 0, nameof(additionalServicesId));

            var record = await _context.HallRent_AdditionalServices.FirstOrDefaultAsync(hras =>
                         (hras.HallRentId == hallRentId && hras.AdditionalServiceId == additionalServicesId)) ??
                         throw new KeyNotFoundException($"Entity with hallRentId {hallRentId} and additionalServicesId {additionalServicesId} does not exist in database.");

            _context.HallRent_AdditionalServices.Remove(record);
        }
    }
}
