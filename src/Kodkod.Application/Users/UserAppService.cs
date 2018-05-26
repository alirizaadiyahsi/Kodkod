using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
