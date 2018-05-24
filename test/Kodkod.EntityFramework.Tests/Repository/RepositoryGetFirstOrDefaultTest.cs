using Kodkod.Core.Entities;
using Kodkod.EntityFramework.Repositories;
using Kodkod.Tests.Shared;
using Xunit;

namespace Kodkod.EntityFramework.Tests.Repository
{
    public class RepositoryGetFirstOrDefaultTest : TestBase
    {
        private readonly IRepository<User> _userRepository;

        public RepositoryGetFirstOrDefaultTest()
        {
            _userRepository = new Repository<User>(GetInitializedDbContext());
        }

        [Fact]
        public async void TestGetFirstOrDefaultAsyncGetsCorrectItem()
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.UserName == "A");
            Assert.NotNull(user);
            Assert.Equal("A", user.UserName);
        }

        [Fact]
        public async void TestGetFirstOrDefaultAsyncReturnsNullValue()
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.UserName == "Easy-E");
            Assert.Null(user);
        }

        [Fact]
        public async void TestGetFirstOrDefaultAsyncCanInclude()
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.UserName == "A");
            Assert.NotNull(user);
        }
    }
}
