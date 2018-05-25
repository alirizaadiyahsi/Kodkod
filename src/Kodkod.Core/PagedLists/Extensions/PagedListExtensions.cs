using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Kodkod.Core.PagedLists.Extensions
{
    public static class PagedListExtensions
    {
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(
            this IQueryable<T> source,
            int pageIndex,
            int pageSize,
            int indexFrom = 0,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }

            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await source.Skip((pageIndex - indexFrom) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            var pagedList = new PagedList<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                IndexFrom = indexFrom,
                TotalCount = count,
                Items = items,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            return pagedList;
        }

        public static IPagedList<T> ToPagedList<T>(
            this IEnumerable<T> source,
            int pageIndex,
            int pageSize,
            int indexFrom = 0
        )
        {
            return new PagedList<T>(source, pageIndex, pageSize, indexFrom);
        }

        public static IPagedList<TResult> ToPagedList<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<IEnumerable<TSource>, IEnumerable<TResult>> converter,
            int pageIndex, int pageSize,
            int indexFrom = 0
        )
        {
            return new PagedList<TSource, TResult>(source, converter, pageIndex, pageSize, indexFrom);
        }
    }
}
