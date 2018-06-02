using System.Collections.Generic;
using System.Threading.Tasks;
using Kodkod.Application.Users.Dto;
using Kodkod.Core.Users;
using Kodkod.EntityFramework.Repositories;

namespace Kodkod.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IRepository<User> _userRepository;

        public UserAppService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        //todo: return paged result and write test for it
        public async Task<List<User>> GetAllAsync(FilterUsersInput input)
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
