using System.Collections.Generic;
using System.Threading.Tasks;
using Kodkod.Core.Entities;
using Kodkod.EntityFramework.Repositories;

namespace Kodkod.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IRepository<ApplicationUser> _userRepository;

        public UserAppService(IRepository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
