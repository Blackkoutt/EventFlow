namespace EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetOne(int id);
        Task Update(T entity);
        Task Delete(int id);
    }
}
