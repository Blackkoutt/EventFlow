namespace EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetOneAsync(int id);
        void Update(T entity);
        Task DeleteAsync(int id);
    }
}
