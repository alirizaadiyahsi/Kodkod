using Kodkod.Core.Users;
using Kodkod.EntityFramework.Repositories;
using Kodkod.Tests.Shared;
using Xunit;

namespace Kodkod.EntityFramework.Tests.Repository
{
    public class RepositoryTests : TestBase
    {
        private readonly IRepository<User> _userRepository;

        public RepositoryTests()
        {
            _userRepository = new Repository<User>(GetInitializedDbContext());
        }

        [Fact]
        public async void TestGetAllAsync()
        {
            var user = await _userRepository.GetAllAsync();
            Assert.Equal(6,user.Count);
        }

        [Fact]
        public async void TestGetFirstOrDefaultAsync()
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.UserName == "A");
            Assert.NotNull(user);
            Assert.Equal("A", user.UserName);
        }
    }
}
