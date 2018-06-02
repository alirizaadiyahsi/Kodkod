using System.Linq;
using System.Threading.Tasks;
using Kodkod.EntityFramework;
using Kodkod.Tests.Shared;
using Kodkod.Utilities.Collections.PagedList.Extensions;
using Xunit;

namespace Kodkod.Utilities.Tests.Collections
{
    public class PageListTests : TestBase
    {
        private readonly KodkodDbContext _kodkodDbContext = GetInitializedDbContext();

        [Fact]
        public async Task ToPagedListAsyncTest()
        {
            var page = await _kodkodDbContext.Users.Where(u => u.UserName != null).ToPagedListAsync(1, 2);
            var totalUserCount = _kodkodDbContext.Users.Count();

            Assert.NotNull(page);
            Assert.Equal(totalUserCount, page.TotalCount);
            Assert.Equal(2, page.Items.Count);
            Assert.Equal("C", page.Items[0].UserName);

            page = await _kodkodDbContext.Users.Where(u => u.UserName != null).ToPagedListAsync(0, 2);

            Assert.NotNull(page);
            Assert.Equal(totalUserCount, page.TotalCount);
            Assert.Equal(2, page.Items.Count);
            Assert.Equal("A", page.Items[0].UserName);
        }
    }
}
