using System.Threading.Tasks;
using Kodkod.Application.Users;
using Kodkod.Application.Users.Dto;
using Kodkod.Core.Users;
using Kodkod.EntityFramework.Repositories;
using Xunit;

namespace Kodkod.Application.Tests
{
    public class UserApplicationServiceTests : ApplicationTestBase
    {
        private readonly IUserAppService _userAppService;

        public UserApplicationServiceTests()
        {
            var userRepository = new Repository<User>(KodkodInMemoryContext);
            _userAppService = new UserAppService(userRepository);
        }

        [Fact]
        public async Task TestGetAllAsync()
        {
            var getUsersInput = new GetUsersInput();
            var usersList = await _userAppService.GetUsersAsync(getUsersInput);
            Assert.True(usersList.Items.Count >= 0);
        }
    }
}
