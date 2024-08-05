using EventFlowAPI.DB.Context;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly APIContext _context;
        private readonly DbSet<T> _table;

        public GenericRepository(APIContext context) 
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            await _table.AddAsync(entity);
            //await _context.SaveChangesAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var records = await _table.ToListAsync();
            return records;
        }
        public virtual async Task<T> GetOneAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }
            var record = await _table.FindAsync(id);
            
            ArgumentNullException.ThrowIfNull(record, nameof(id));  

            return record;
        }
        public void Update(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _table.Update(entity);
            //await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var record = await _table.FindAsync(id);
            ArgumentNullException.ThrowIfNull(record, nameof(id));

            _table.Remove(record);

            //await _context.SaveChangesAsync();
        }

    }
}
