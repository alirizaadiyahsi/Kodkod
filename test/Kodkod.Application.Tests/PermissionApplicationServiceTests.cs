using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kodkod.Application.Permissions;
using Kodkod.Core.Permissions;
using Kodkod.Core.Users;
using Kodkod.EntityFramework;
using Kodkod.EntityFramework.Repositories;
using Kodkod.Tests.Shared;
using Xunit;

namespace Kodkod.Application.Tests
{
    public class PermissionApplicationServiceTests : TestBase
    {
        private readonly IPermissionAppService _permissionAppService;
        private readonly ClaimsPrincipal _contextUser;
        private readonly KodkodDbContext _kodkodDbContext = GetInitializedDbContext();

        public PermissionApplicationServiceTests()
        {
            var userRepository = new Repository<User>(_kodkodDbContext);
            var permissionRepository = new Repository<Permission>(_kodkodDbContext);

            _permissionAppService = new PermissionAppService(userRepository, permissionRepository);
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
        public async void TestGetAllAsync()
        {
            var permissions = await _permissionAppService.GetAllAsync();
            Assert.NotNull(permissions);
            Assert.True(permissions.Count >= 0);
        }

        [Fact]
        public async Task TestIsPermissionGrantedForUserAsync()
        {
            var isPermissionGranted = await _permissionAppService.IsPermissionGrantedForUserAsync(_contextUser, ApiUserPermission);

            Assert.True(isPermissionGranted);
        }

        [Fact]
        public async Task TestInitializePermissions()
        {
            var testPermission = new Permission
            {
                Id = Guid.NewGuid(),
                Name = "TestPermission",
                DisplayName = "Test permission"
            };

            var permissions = PermissionConsts.AllPermissions();
            permissions.Add(testPermission);

            await _permissionAppService.InitializePermissions(permissions);
            await _kodkodDbContext.SaveChangesAsync();

            var latestPermissionsCount = (await _permissionAppService.GetAllAsync()).Count;
            Assert.Equal(latestPermissionsCount, PermissionConsts.AllPermissions().Count + 1);
        }
    }
}
