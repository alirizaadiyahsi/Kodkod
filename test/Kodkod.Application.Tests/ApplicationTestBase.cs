using System.Collections.Generic;
using System.Security.Claims;
using Kodkod.EntityFramework;
using Kodkod.Tests.Shared;

namespace Kodkod.Application.Tests
{
    public class ApplicationTestBase : TestBase
    {
        protected readonly KodkodDbContext KodkodInMemoryContext = GetInitializedDbContext();

        protected readonly ClaimsPrincipal ContextUser = new ClaimsPrincipal(
            new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.Name, TestUser.UserName)
                }
            )
        );
    }
}
