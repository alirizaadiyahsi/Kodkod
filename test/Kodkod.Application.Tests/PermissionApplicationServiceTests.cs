using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Kodkod.Application.Permissions;
using Kodkod.Application.Users;
using Kodkod.Core.Entities;
using Kodkod.EntityFramework;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Kodkod.Application.Tests
{
    public class PermissionApplicationServiceTests : TestBase
    {
        private readonly IPermissionApplicationService _permissionApplicationService;
        private readonly ClaimsPrincipal _contextUser;

        public PermissionApplicationServiceTests()
        {
            var permissionRepository = Substitute.For<IRepository<Permission>>();
            permissionRepository.GetAllAsync()
                .Returns(GetInitializedDbContext().Permissions.ToListAsync());
            _permissionApplicationService = new PermissionApplicationService(permissionRepository);
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
            var isPermissionGranted = _permissionApplicationService.CheckPermissionForUser(_contextUser, ApiUserPermission);
            Assert.True(isPermissionGranted);
        }
    }
}
