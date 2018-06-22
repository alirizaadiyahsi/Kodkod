using System.Collections.Generic;
using System.Threading.Tasks;
using Kodkod.Application.Permissions;
using Kodkod.Core.Permissions;
using Kodkod.Core.Users;
using Kodkod.EntityFramework.Repositories;
using Kodkod.Web.Core.Authentication;
using Microsoft.AspNetCore.Authorization;
using Xunit;

namespace Kodkod.Web.Api.Tests
{
    public class AuthorizationHandlerTests : ApiTestBase
    {
        [Fact]
        public async Task TestPermissionHandler()
        {
            var userRepository = new Repository<User>(KodkodInMemoryContext);
            var permissionRepository = new Repository<Permission>(KodkodInMemoryContext);
            var permissionAppService = new PermissionAppService(userRepository, permissionRepository, Mapper);

            var requirements = new List<PermissionRequirement>
            {
                new PermissionRequirement(ApiUserPermission)
            };
            var authorizationHandlerContext = new AuthorizationHandlerContext(requirements, ContextUser, null);
            var permissionHandler = new PermissionHandler(permissionAppService);
            await permissionHandler.HandleAsync(authorizationHandlerContext);

            Assert.True(authorizationHandlerContext.HasSucceeded);
        }
    }
}
