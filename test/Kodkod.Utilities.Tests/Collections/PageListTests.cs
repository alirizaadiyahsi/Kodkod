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
            var pagedList = await _kodkodDbContext.Users.Where(u => u.UserName != null).ToPagedListAsync(1, 2);
            var totalUserCount = _kodkodDbContext.Users.Count();

            Assert.NotNull(pagedList);
            Assert.Equal(totalUserCount, pagedList.TotalCount);
            Assert.Equal(2, pagedList.Items.Count);
            Assert.Equal("C", pagedList.Items[0].UserName);

            pagedList = await _kodkodDbContext.Users.Where(u => u.UserName != null).ToPagedListAsync(0, 2);

            Assert.NotNull(pagedList);
            Assert.Equal(totalUserCount, pagedList.TotalCount);
            Assert.Equal(2, pagedList.Items.Count);
            Assert.Equal("A", pagedList.Items[0].UserName);
        }
    }
}
