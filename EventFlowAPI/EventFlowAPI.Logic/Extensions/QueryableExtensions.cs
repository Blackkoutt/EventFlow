using EventFlowAPI.DB.Entities;
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

        public static IQueryable<Reservation> ReservationsByStatus(this IQueryable<Reservation> queryable, ReservationStatus? status)
        {
            return status switch
            {
                ReservationStatus.Active => queryable.Where(r => (!r.IsCanceled && r.EndOfReservationDate > DateTime.Now)),
                ReservationStatus.Expired => queryable.Where(r => (!r.IsCanceled && !(r.EndOfReservationDate > DateTime.Now))),
                ReservationStatus.Canceled => queryable.Where(r => (r.IsCanceled)),
                _ => queryable
            };
        }

        public static IQueryable<EventPass> EventPassByStatus(this IQueryable<EventPass> queryable, EventPassStatus? status)
        {
            return status switch
            {
                EventPassStatus.Active => queryable.Where(r => (!r.IsCanceled && r.EndDate > DateTime.Now)),
                EventPassStatus.Expired => queryable.Where(r => (!r.IsCanceled && !(r.EndDate > DateTime.Now))),
                EventPassStatus.Canceled => queryable.Where(r => (r.IsCanceled)),
                _ => queryable
            };
        }

        public static IQueryable<Event> EventByStatus(this IQueryable<Event> queryable, EventStatus? status)
        {
            return status switch
            {
                EventStatus.Active => queryable.Where(r => (!r.IsCanceled && r.EndDate > DateTime.Now)),
                EventStatus.Expired => queryable.Where(r => (!r.IsCanceled && !(r.EndDate > DateTime.Now))),
                EventStatus.Canceled => queryable.Where(r => (r.IsCanceled)),
                _ => queryable
            };
        }

        public static IQueryable<HallRent> HallRentByStatus(this IQueryable<HallRent> queryable, HallRentStatus? status)
        {
            return status switch
            {
                HallRentStatus.Active => queryable.Where(r => (!r.IsCanceled && r.EndDate > DateTime.Now)),
                HallRentStatus.Expired => queryable.Where(r => (!r.IsCanceled && !(r.EndDate > DateTime.Now))),
                HallRentStatus.Canceled => queryable.Where(r => (r.IsCanceled)),
                _ => queryable
            };
        }
    }
}
