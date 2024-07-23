namespace EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetOne(int id);
        void Update(T entity);
        Task Delete(int id);
    }
}
