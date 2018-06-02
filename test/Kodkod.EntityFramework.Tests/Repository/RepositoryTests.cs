using System;
using System.Linq;
using Kodkod.Core.Users;
using Kodkod.EntityFramework.Repositories;
using Kodkod.Tests.Shared;
using Xunit;

namespace Kodkod.EntityFramework.Tests.Repository
{
    public class RepositoryTests : TestBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly KodkodDbContext _kodkodInMemoryContext = GetInitializedDbContext();
        private readonly int _userCount;

        public RepositoryTests()
        {
            _userCount = _kodkodInMemoryContext.Users.Count();
            _userRepository = new Repository<User>(_kodkodInMemoryContext);
        }

        [Fact]
        public async void TestGetAllAsync()
        {
            var userList = await _userRepository.GetAllAsync();
            Assert.Equal(_userCount, userList.Count);
        }

        //todo: remove these line after imlementaion of paged list on appservices
        //[Fact]
        //public async void TestGetPagedListAsync()
        //{
        //    var pagedUserList = await _userRepository.GetPagedListAsync(pageIndex: 1, pageSize: 2);
        //    Assert.Equal(2, pagedUserList.Items.Count);
        //    Assert.Equal(1, pagedUserList.PageIndex);
        //}

        [Fact]
        public async void TestGetFirstOrDefaultAsync()
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.UserName == "A");
            Assert.NotNull(user);
            Assert.Equal("A", user.UserName);
        }

        [Fact]
        public async void TestInsertAsync()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "testuser_for_insert"
            };

            await _userRepository.InsertAsync(user);
            await _kodkodInMemoryContext.SaveChangesAsync();

            var insertedUser = await _userRepository.GetFirstOrDefaultAsync(u => u.UserName == user.UserName);
            Assert.NotNull(insertedUser);
            Assert.Equal(user.UserName, insertedUser.UserName);
        }

        [Fact]
        public async void TestBatchInsertAsync()
        {
            var user1 = new User
            {
                Id = Guid.NewGuid(),
                UserName = "testuser_1"
            };

            var user2 = new User
            {
                Id = Guid.NewGuid(),
                UserName = "testuser_2"
            };

            await _userRepository.InsertAsync(new[] { user1, user2 });
            await _kodkodInMemoryContext.SaveChangesAsync();

            var insertedUser1 = await _userRepository.GetFirstOrDefaultAsync(u => u.UserName == user1.UserName);
            Assert.NotNull(insertedUser1);
            Assert.Equal(user1.UserName, insertedUser1.UserName);

            var insertedUser2 = await _userRepository.GetFirstOrDefaultAsync(u => u.UserName == user2.UserName);
            Assert.NotNull(insertedUser2);
            Assert.Equal(user2.UserName, insertedUser2.UserName);
        }
    }
}
