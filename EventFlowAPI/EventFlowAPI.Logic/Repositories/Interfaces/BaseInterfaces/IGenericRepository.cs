using EventFlowAPI.DB.Entities.Abstract;

namespace EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces
{
    public interface IGenericRepository<T> : IRepository where T : class
    {
        Task AddAsync(IEntity? entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetOneAsync(int id);
        void Update(IEntity? entity);
        Task DeleteAsync(int id);
    }
}
