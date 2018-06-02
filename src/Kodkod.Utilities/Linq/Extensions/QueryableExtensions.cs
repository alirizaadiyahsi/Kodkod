using System;
using System.Linq;
using System.Linq.Expressions;

namespace Kodkod.Utilities.Linq.Extensions
{
    //todo: remove this extension if not needed/used
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }
    }
}
