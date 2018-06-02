using System.Threading.Tasks;
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
        private readonly KodkodDbContext _kodkodInMemoryContext = GetInitializedDbContext();

        public UserApplicationServiceTests()
        {
            var userRepository = new Repository<User>(_kodkodInMemoryContext);
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
