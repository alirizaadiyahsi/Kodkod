using Kodkod.Application.Users;
using Kodkod.Core.Entities;
using Kodkod.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Kodkod.Application.Tests
{
    public class UserApplicationServiceTests : TestBase
    {
        private readonly IUserApplicationService _userApplicationService;

        public UserApplicationServiceTests()
        {
            var userRepository = Substitute.For<IRepository<ApplicationUser>>();
            userRepository.GetAllAsync()
                .Returns(GetInitializedDbContext().Users.ToListAsync());
            _userApplicationService = new UserApplicationService(userRepository);
        }

        [Fact]
        public async void TestGetAll()
        {
            var users = await _userApplicationService.GetAllAsync();
            Assert.NotNull(users);
            Assert.Equal(6, users.Count);
        }
    }
}
