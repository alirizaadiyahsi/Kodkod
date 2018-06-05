using System.Linq;
using Kodkod.Utilities.Linq.Extensions;
using Xunit;

namespace Kodkod.Utilities.Tests.Linq
{
    public class QueryableTests : UtilitiesTestBase
    {
        [Fact]
        public void ToPagedListAsyncTest()
        {
            var users = KodkodInMemoryContext.Users;
            var pagedUserList = users.PagedBy(0, 2);

            Assert.NotNull(pagedUserList);
            Assert.Equal(2, pagedUserList.Count());
        }
    }
}
