using EventFlowAPI.DB.Context;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.UnitOfWork
{
    public interface IUnitOfWork 
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task SaveChangesAsync();
        APIContext Context { get; }
    }
}
