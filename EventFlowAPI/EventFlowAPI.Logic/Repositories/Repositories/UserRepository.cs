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
            return await _context.User
                        .Include(u => u.UserData)
                        .AsSplitQuery()
                        .ToListAsync();
        }
        public override sealed async Task<User?> GetOneAsync(int id)
        {
            return await _context.User
                        .AsSplitQuery()
                        .Include(u => u.UserData)
                        .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
