using System.Threading.Tasks;
using Kodkod.Application.Users.Dto;
using Kodkod.Core.Users;
using Kodkod.Utilities.PagedList;

namespace Kodkod.Application.Users
{
    public interface IUserAppService
    {
        Task<IPagedList<User>> GetUsersAsync(GetUsersInput input);
    }
}