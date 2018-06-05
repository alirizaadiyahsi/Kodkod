using System.Threading.Tasks;
using Kodkod.Utilities.Linq.Extensions;
using Kodkod.Utilities.PagedList.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Kodkod.Utilities.Tests.Collections
{
    public class PageListTests : UtilitiesTestBase
    {
        [Fact]
        public async Task ToPagedListAsyncTest()
        {
            var users = KodkodInMemoryContext.Users;
            var usersCount = await users.CountAsync();
            var pagedUserList = users.PagedBy(1, 2).ToPagedList(usersCount, 1, 2);

            Assert.NotNull(pagedUserList);
            Assert.Equal(usersCount, pagedUserList.TotalCount);
            Assert.Equal(2, pagedUserList.Items.Count);
            Assert.Equal(1, pagedUserList.PageIndex);

            pagedUserList = users.PagedBy(0, 3).ToPagedList(usersCount, 0, 3);

            Assert.NotNull(pagedUserList);
            Assert.Equal(usersCount, pagedUserList.TotalCount);
            Assert.Equal(3, pagedUserList.Items.Count);
            Assert.Equal(0, pagedUserList.PageIndex);
        }
    }
}
