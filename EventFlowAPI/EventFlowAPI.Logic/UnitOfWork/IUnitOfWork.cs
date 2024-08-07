using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.UnitOfWork
{
    public interface IUnitOfWork 
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task SaveChangesAsync();
    }
}
