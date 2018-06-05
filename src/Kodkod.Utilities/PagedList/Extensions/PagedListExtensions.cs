using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Kodkod.Utilities.PagedList.Extensions
{
    public static class PagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(
            this IEnumerable<T> source,
            int count,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var pagedList = new PagedList<T>
            {
                TotalCount = count,
                Items = source.ToList()
            };

            return pagedList;
        }
    }
}
