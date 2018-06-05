using System.Collections.Generic;
using System.Linq;
using Kodkod.Application.Users.Dto;
using Kodkod.Core.Users;
using Kodkod.EntityFramework.Repositories;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using Kodkod.Utilities.PagedList;
using Kodkod.Utilities.PagedList.Extensions;
using Kodkod.Utilities.Extensions;
using Kodkod.Utilities.Linq.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Kodkod.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IRepository<User> _userRepository;

        public UserAppService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IPagedList<UserListDto>> GetUsersAsync(GetUsersInput input)
        {
            var query = _userRepository.GetAll(
                    !input.Filter.IsNullOrEmpty(),
                    predicate => predicate.UserName.Contains(input.Filter) ||
                                 predicate.Email.Contains(input.Filter))
                .OrderBy(input.Sorting);

            var usersCount = await query.CountAsync();
            var users = query.PagedBy(input.PageIndex, input.PageSize).ToList();
            var userListDtos = Mapper.Map<List<UserListDto>>(users);

            return userListDtos.ToPagedList(usersCount, input.PageIndex, input.PageSize);
        }
    }
}
