using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Models;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public class UserRepository(APIContext context) : Repository<User>(context), IUserRepository
    {
        public override sealed async Task<IEnumerable<User>> GetAll()
        {
            var records = await _context.User
                                .Include(u => u.UserData)
                                .AsSplitQuery()
                                .ToListAsync();

            return records;
        }
        public override sealed async Task<User> GetOne(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var record = await _context.User
                                .AsSplitQuery()
                                .Include(u => u.UserData)
                                .FirstOrDefaultAsync(e => e.Id == id);


            ArgumentNullException.ThrowIfNull(record, nameof(id));

            return record;
        }
    }
}
