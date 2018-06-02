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
            var pagedUserList = await _kodkodDbContext.Users.Where(u => u.UserName != null).ToPagedListAsync(1, 2);
            var totalUserCount = _kodkodDbContext.Users.Count();

            Assert.NotNull(pagedUserList);
            Assert.Equal(totalUserCount, pagedUserList.TotalCount);
            Assert.Equal(2, pagedUserList.Items.Count);
            Assert.Equal(1, pagedUserList.PageIndex);

            pagedUserList = await _kodkodDbContext.Users.Where(u => u.UserName != null).ToPagedListAsync(0, 3);

            Assert.NotNull(pagedUserList);
            Assert.Equal(totalUserCount, pagedUserList.TotalCount);
            Assert.Equal(3, pagedUserList.Items.Count);
            Assert.Equal(0, pagedUserList.PageIndex);
        }
    }
}
