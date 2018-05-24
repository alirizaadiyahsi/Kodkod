using Kodkod.Application.Users;
using Kodkod.Core.Entities;
using Kodkod.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Kodkod.Application.Tests
{
    public class UserApplicationServiceTests : TestBase
    {
        private readonly IUserAppService _userAppService;

        public UserApplicationServiceTests()
        {
            var userRepository = Substitute.For<IRepository<User>>();
            userRepository.GetAllAsync()
                .Returns(GetInitializedDbContext().Users.ToListAsync());
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
