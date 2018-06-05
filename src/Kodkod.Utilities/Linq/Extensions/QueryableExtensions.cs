using System;
using System.Linq;
using System.Threading;

namespace Kodkod.Utilities.Linq.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> PagedBy<T>(
            this IQueryable<T> source,
            int pageIndex,
            int pageSize,
            int indexFrom = 0,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }

            return source.Skip((pageIndex - indexFrom) * pageSize).Take(pageSize);
        }
    }
}
