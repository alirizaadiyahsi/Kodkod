using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Kodkod.EntityFramework.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(
            bool condition = false,
            Expression<Func<TEntity, bool>> predicate = null,
            bool disableTracking = false);

        Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            bool disableTracking = false);

        Task InsertAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken));

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        void Delete(IEnumerable<TEntity> entities);
    }
}
