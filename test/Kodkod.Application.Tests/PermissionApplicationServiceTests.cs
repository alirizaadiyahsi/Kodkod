using System.Collections.Generic;
using System.Security.Claims;
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
            var permissionRepository = Substitute.For<IRepository<Permission>>();
            permissionRepository.GetAllAsync()
                .Returns(GetInitializedDbContext().Permissions.ToListAsync());
            _permissionAppService = new PermissionAppService(permissionRepository);
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
        public void TestCheckPermissionForUser()
        {
            var isPermissionGranted = _permissionAppService.CheckPermissionForUser(_contextUser, ApiUserPermission);
            Assert.True(isPermissionGranted);
        }
    }
}
