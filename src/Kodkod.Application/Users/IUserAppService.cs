using System.Collections.Generic;
using System.Threading.Tasks;
using Kodkod.Core.Users;

namespace Kodkod.Application.Users
{
    public interface IUserAppService
    {
        Task<List<User>> GetAllAsync();
    }
}