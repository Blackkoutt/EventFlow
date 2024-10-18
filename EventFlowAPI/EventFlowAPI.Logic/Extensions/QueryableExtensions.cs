using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.Helpers.Enums;
using System.Linq.Expressions;
using System.Reflection;

namespace EventFlowAPI.Logic.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> SortBy<TEntity>(this IQueryable<TEntity> query, string? sortBy, SortDirection sortDirection) where TEntity : class
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                PropertyInfo? property = typeof(TEntity)
                                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        .FirstOrDefault(p => p.Name.Equals(sortBy, StringComparison.OrdinalIgnoreCase));
                if (property == null) return query;

                var parameter = Expression.Parameter(typeof(TEntity), "x");

                // Create the property access expression (x.PropertyName)
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);

                // Create the sorting lambda expression (x => x.PropertyName)
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);

                // Get method name
                string methodName = (sortDirection == SortDirection.DESC) ? "OrderByDescending" : "OrderBy";

                // Use reflection to get the correct method
                MethodInfo orderByMethod = typeof(Queryable).GetMethods()
                .Where(m => m.Name == methodName && m.GetParameters().Length == 2)
                    .Single()
                    .MakeGenericMethod(typeof(TEntity), property.PropertyType);

                var invokedMethodResult = orderByMethod.Invoke(
                            obj: null,
                            parameters: new object[] { query, orderByExpression });

                if (invokedMethodResult == null)
                    throw new Exception();

                return (IQueryable<TEntity>)invokedMethodResult;

            }
            return query;
        }

        public static IQueryable<TEntity> ByStatus<TEntity>(this IQueryable<TEntity> queryable, Status? status) where TEntity : class
        {
            return status switch
            {
                Status.Active => queryable.Where(r => (!((ISoftDeleteable)r).IsCanceled && ((IDateableEntity)r).EndDate > DateTime.Now)),
                Status.Expired => queryable.Where(r => (!((ISoftDeleteable)r).IsCanceled && !(((IDateableEntity)r).EndDate > DateTime.Now))),
                Status.Canceled => queryable.Where(r => (((ISoftDeleteable)r).IsCanceled)),
                _ => queryable
            };
        }
    }
}
