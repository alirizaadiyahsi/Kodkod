using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Kodkod.Application.Permissions;
using Kodkod.Core.Users;
using Kodkod.EntityFramework.Repositories;
using Kodkod.Tests.Shared;
using Xunit;

namespace Kodkod.Application.Tests
{
    public class PermissionApplicationServiceTests : TestBase
    {
        private readonly IPermissionAppService _permissionAppService;
        private readonly ClaimsPrincipal _contextUser;

        public PermissionApplicationServiceTests()
        {
            var userRepository = new Repository<User>(GetInitializedDbContext());
            _permissionAppService = new PermissionAppService(userRepository);
            _contextUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, TestUser.UserName)
                    }
                )
            );
        }

        [Fact]
        public async Task TestCheckPermissionForUser()
        {
            var isPermissionGranted = await _permissionAppService.CheckPermissionForUserAsync(_contextUser, ApiUserPermission);

            Assert.True(isPermissionGranted);
        }
    }
}
