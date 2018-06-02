using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Kodkod.EntityFramework.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly KodkodDbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(KodkodDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll(
            bool condition = false,
            Expression<Func<TEntity, bool>> predicate = null,
            bool disableTracking = false)
        {
            IQueryable<TEntity> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (condition && predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            bool disableTracking = false)
        {
            IQueryable<TEntity> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.FirstOrDefaultAsync();
        }

        public Task InsertAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return DbSet.AddRangeAsync(entities, cancellationToken);
        }

        public Task InsertAsync(
            TEntity entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return DbSet.AddAsync(entity, cancellationToken);
        }
    }
}