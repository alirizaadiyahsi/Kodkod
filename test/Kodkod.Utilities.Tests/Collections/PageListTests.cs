using System.Linq;
using System.Threading.Tasks;
using Kodkod.EntityFramework;
using Kodkod.Utilities.PagedList.Extensions;
using Xunit;

namespace Kodkod.Utilities.Tests.Collections
{
    public class PageListTests : UtilitiesTestBase
    {
        [Fact]
        public async Task ToPagedListAsyncTest()
        {
            var pagedUserList = await KodkodInMemoryContext.Users.Where(u => u.UserName != null).ToPagedListAsync(1, 2);
            var totalUserCount = KodkodInMemoryContext.Users.Count();

            Assert.NotNull(pagedUserList);
            Assert.Equal(totalUserCount, pagedUserList.TotalCount);
            Assert.Equal(2, pagedUserList.Items.Count);
            Assert.Equal(1, pagedUserList.PageIndex);

            pagedUserList = await KodkodInMemoryContext.Users.Where(u => u.UserName != null).ToPagedListAsync(0, 3);

            Assert.NotNull(pagedUserList);
            Assert.Equal(totalUserCount, pagedUserList.TotalCount);
            Assert.Equal(3, pagedUserList.Items.Count);
            Assert.Equal(0, pagedUserList.PageIndex);
        }
    }
}
