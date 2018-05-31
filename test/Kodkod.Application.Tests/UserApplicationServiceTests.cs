using Kodkod.Application.Users;
using Kodkod.Application.Users.Dto;
using Kodkod.Core.Users;
using Kodkod.EntityFramework;
using Kodkod.EntityFramework.Repositories;
using Kodkod.Tests.Shared;
using Xunit;

namespace Kodkod.Application.Tests
{
    public class UserApplicationServiceTests : TestBase
    {
        private readonly IUserAppService _userAppService;
        private readonly KodkodDbContext _kodkodDbContext = GetInitializedDbContext();

        public UserApplicationServiceTests()
        {
            var userRepository = new Repository<User>(_kodkodDbContext);
            _userAppService = new UserAppService(userRepository);
        }

        [Fact]
        public async void TestGetAllAsync()
        {
            //todo: fill FilterUsersInput object
            var users = await _userAppService.GetAllAsync(new FilterUsersInput());
            Assert.NotNull(users);
            Assert.True(users.Count >= 0);
        }
    }
}
