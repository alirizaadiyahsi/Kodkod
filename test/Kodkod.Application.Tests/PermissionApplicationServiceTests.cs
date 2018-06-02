using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Kodkod.Application.Permissions;
using Kodkod.Application.Permissions.Dto;
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
        private readonly KodkodDbContext _kodkodInMemoryContext = GetInitializedDbContext();

        public PermissionApplicationServiceTests()
        {
            var userRepository = new Repository<User>(_kodkodInMemoryContext);
            var permissionRepository = new Repository<Permission>(_kodkodInMemoryContext);
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
        public async void TestGetPermissionsAsync()
        {
            var getPermissionsInput = new GetPermissionsInput();
            var permissionsList = await _permissionAppService.GetPermissionsAsync(getPermissionsInput);
            Assert.True(permissionsList.Items.Count >= 0);

            getPermissionsInput.Filter = "qwerty";
            var permissionsListEmpty = await _permissionAppService.GetPermissionsAsync(getPermissionsInput);
            Assert.True(permissionsListEmpty.Items.Count == 0);
        }

        [Fact]
        public async Task TestIsPermissionGrantedForUserAsync()
        {
            var isPermissionGranted = await _permissionAppService.IsPermissionGrantedForUserAsync(_contextUser, ApiUserPermission);
            Assert.True(isPermissionGranted);
        }

        [Fact]
        public async Task TestInitializePermissionsAsync()
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
            await _kodkodInMemoryContext.SaveChangesAsync();

            var initializedPermission = _permissionAppService.GetFirstOrDefaultAsync(testPermission.Id);
            Assert.NotNull(initializedPermission);
        }
    }
}
