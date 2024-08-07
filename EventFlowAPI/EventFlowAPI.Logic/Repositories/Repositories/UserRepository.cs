using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class UserRepository(APIContext context) : GenericRepository<User>(context), IUserRepository
    {
        public override sealed async Task<IEnumerable<User>> GetAllAsync()
        {
            var records = await _context.User
                                .Include(u => u.UserData)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public override sealed async Task<User> GetOneAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 0, nameof(id));

            var record = await _context.User
                                .AsSplitQuery()
                                .Include(u => u.UserData)
                                .FirstOrDefaultAsync(e => e.Id == id);

            return record ?? throw new KeyNotFoundException($"Entity with id {id} does not exist in database."); ;
        }
    }
}
