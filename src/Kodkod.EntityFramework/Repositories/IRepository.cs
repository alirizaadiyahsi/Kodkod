using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Kodkod.Utilities.Collections.PagedList;
using Microsoft.EntityFrameworkCore.Query;

namespace Kodkod.EntityFramework.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = false);

        //Task<IPagedList<TEntity>> GetPagedListAsync(
        //    Expression<Func<TEntity, bool>> predicate = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        //    int pageIndex = 0,
        //    int pageSize = 20,
        //    bool disableTracking = false,
        //    CancellationToken cancellationToken = default(CancellationToken));

        Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = false);

        Task InsertAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken));

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}
