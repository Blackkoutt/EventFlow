﻿using EventFlowAPI.DB.Context;
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

        public async Task AddAsync(T entity) => await _table.AddAsync(entity);

        public virtual IQueryable<T> GetAllQueryable() => _table.AsQueryable();
        public virtual async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? query = null)
            => await (query?.Invoke(_table) ?? _table).ToListAsync();
        public virtual async Task<T?> GetOneAsync(int id) => await _table.FindAsync(id);
        public void Update(T entity) => _table.Update(entity);
        public void Delete(T entity) => _table.Remove(entity);

    }
}
