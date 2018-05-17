using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kodkod.EntityFramework
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
    }
}
