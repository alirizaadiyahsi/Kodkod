using System.Linq;
using System.Threading.Tasks;
using Kodkod.EntityFramework;
using Kodkod.Utilities.PagedList.Extensions;
using Xunit;

namespace Kodkod.Utilities.Tests.Collections
{
    public class PageListTests : UtilitiesTestBase
    {
        //todo: write test paged list and pagedby(in different class) test

        //[Fact]
        //public async Task ToPagedListAsyncTest()
        //{
        //    var users = await KodkodInMemoryContext.Users.Where(u => u.UserName != null);
        //    var usersCount = users.CountAsync();
        //    users.ToPagedList(usersCount, 1, 2);

        //    var totalUserCount = KodkodInMemoryContext.Users.Count();

        //    Assert.NotNull(pagedUserList);
        //    Assert.Equal(totalUserCount, pagedUserList.TotalCount);
        //    Assert.Equal(2, pagedUserList.Items.Count);
        //    Assert.Equal(1, pagedUserList.PageIndex);

        //    pagedUserList = await KodkodInMemoryContext.Users.Where(u => u.UserName != null).ToPagedList(0, 3);

        //    Assert.NotNull(pagedUserList);
        //    Assert.Equal(totalUserCount, pagedUserList.TotalCount);
        //    Assert.Equal(3, pagedUserList.Items.Count);
        //    Assert.Equal(0, pagedUserList.PageIndex);
        //}
    }
}
