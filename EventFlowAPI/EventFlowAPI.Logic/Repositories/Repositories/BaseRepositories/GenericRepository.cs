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

        public async Task AddAsync(IEntity entity) => await _table.AddAsync((T)entity);
        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _table.ToListAsync();
        public virtual async Task<T?> GetOneAsync(int id) => await _table.FindAsync(id);
        public void Update(IEntity entity) => _table.Update((T)entity);
        public void Delete(IEntity entity) => _table.Remove((T)entity);

    }
}
