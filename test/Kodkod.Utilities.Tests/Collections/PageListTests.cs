using System.Threading.Tasks;
using Kodkod.Utilities.PagedList.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Kodkod.Utilities.Tests.Collections
{
    public class PageListTests : UtilitiesTestBase
    {
        [Fact]
        public async Task ToPagedListTest()
        {
            var users = KodkodInMemoryContext.Users;
            var usersCount = await users.CountAsync();
            var pagedUserList = users.ToPagedList(usersCount);

            Assert.NotNull(pagedUserList);
            Assert.Equal(usersCount, pagedUserList.TotalCount);
            Assert.Equal(usersCount, pagedUserList.Items.Count);
        }
    }
}
