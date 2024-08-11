using EventFlowAPI.DB.Entities.Abstract;

namespace EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces
{
    public interface IGenericRepository<T> : IRepository where T : class
    {
        Task AddAsync(IEntity entity);
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? query = null);
        Task<T?> GetOneAsync(int id);
        void Update(IEntity entity);
        void Delete(IEntity id);
    }
}
