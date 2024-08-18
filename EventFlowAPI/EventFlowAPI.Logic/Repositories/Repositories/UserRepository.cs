using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class UserRepository(APIContext context) : GenericRepository<User>(context), IUserRepository
    {
        public sealed override async Task<IEnumerable<User>> GetAllAsync(Func<IQueryable<User>, IQueryable<User>>? query = null)
        {
            var _table = _context.User
                        .Include(u => u.UserData)
                        .AsSplitQuery();

            return await (query != null ? query(_table).ToListAsync() : _table.ToListAsync());
        }

        public sealed override async Task<User?> GetOneAsync(int id)
        {
            return await _context.User
                        .AsSplitQuery()
                        .Include(u => u.UserData)
                        .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
