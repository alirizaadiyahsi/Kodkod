using Kodkod.Application.Users;
using Kodkod.Core.Users;
using Kodkod.EntityFramework.Repositories;
using Kodkod.Tests.Shared;
using Xunit;

namespace Kodkod.Application.Tests
{
    public class UserApplicationServiceTests : TestBase
    {
        private readonly IUserAppService _userAppService;

        public UserApplicationServiceTests()
        {
            var userRepository = new Repository<User>(GetInitializedDbContext());

            _userAppService = new UserAppService(userRepository);
        }

        [Fact]
        public async void TestGetAll()
        {
            var users = await _userAppService.GetAllAsync();
            Assert.NotNull(users);
            Assert.Equal(6, users.Count);
        }
    }
}
