using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly APIContext _context;
        private readonly DbSet<T> _table;

        protected GenericRepository(APIContext context) 
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task AddAsync(IEntity? entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            await _table.AddAsync((T)entity);
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var records = await _table.ToListAsync();
            return records;
        }
        public virtual async Task<T> GetOneAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 0, nameof(id));

            return await _table.FindAsync(id) ?? throw new KeyNotFoundException($"Entity with id {id} does not exist in database.");
        }
        public void Update(IEntity? entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _table.Update((T)entity);
        }
        public async Task DeleteAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(id, 0, nameof(id));

            var record = await _table.FindAsync(id) ?? throw new KeyNotFoundException($"Entity with id {id} does not exist in database.");

            _table.Remove(record);
        }

    }
}
