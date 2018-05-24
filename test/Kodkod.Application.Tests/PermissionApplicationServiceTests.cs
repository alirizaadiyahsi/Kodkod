using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Kodkod.Application.Permissions;
using Kodkod.Core.Entities;
using Kodkod.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Kodkod.Application.Tests
{
    public class PermissionApplicationServiceTests : TestBase
    {
        private readonly IPermissionAppService _permissionAppService;
        private readonly ClaimsPrincipal _contextUser;

        public PermissionApplicationServiceTests()
        {
            var userRepository = Substitute.For<IRepository<User>>();
            userRepository.GetAllAsync()
                .Returns(GetInitializedDbContext().Users.ToListAsync());
            _permissionAppService = new PermissionAppService(userRepository);
            _contextUser = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, AdminUser.UserName)
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
