using System.Collections.Generic;
using System.Threading.Tasks;
using Kodkod.Core.Entities;

namespace Kodkod.Application.Users
{
    public interface IUserAppService
    {
        Task<List<User>> GetAllAsync();
    }
}