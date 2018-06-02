using Kodkod.Application.Users.Dto;
using Kodkod.Core.Users;
using Kodkod.EntityFramework.Repositories;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Kodkod.Utilities.PagedList;
using Kodkod.Utilities.PagedList.Extensions;
using Kodkod.Utilities.Extensions;

namespace Kodkod.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IRepository<User> _userRepository;

        public UserAppService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IPagedList<User>> GetUsersAsync(GetUsersInput input)
        {
            var query = _userRepository.GetAll(
                    !input.Filter.IsNullOrEmpty(),
                    predicate => predicate.UserName.Contains(input.Filter) ||
                                 predicate.Email.Contains(input.Filter))
                .OrderBy(input.Sorting);

            return await query.ToPagedListAsync(input.PageIndex, input.PageSize);
        }
    }
}
