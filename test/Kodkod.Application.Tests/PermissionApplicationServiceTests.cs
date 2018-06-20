using System;
using System.Linq;
using System.Threading.Tasks;
using Kodkod.Application.Permissions;
using Kodkod.Application.Permissions.Dto;
using Kodkod.Core.Permissions;
using Kodkod.Core.Users;
using Kodkod.EntityFramework.Repositories;
using Xunit;

namespace Kodkod.Application.Tests
{
    public class PermissionApplicationServiceTests : ApplicationTestBase
    {
        private readonly IPermissionAppService _permissionAppService;

        public PermissionApplicationServiceTests()
        {
            var userRepository = new Repository<User>(KodkodInMemoryContext);
            var permissionRepository = new Repository<Permission>(KodkodInMemoryContext);
            _permissionAppService = new PermissionAppService(userRepository, permissionRepository);
        }

        [Fact]
        public async void TestGetPermissions()
        {
            var getPermissionsInput = new GetPermissionsInput();
            var permissionsList = await _permissionAppService.GetPermissionsAsync(getPermissionsInput);
            Assert.True(permissionsList.Items.Count >= 0);

            getPermissionsInput.Filter = "qwerty";
            var permissionsListEmpty = await _permissionAppService.GetPermissionsAsync(getPermissionsInput);
            Assert.True(permissionsListEmpty.Items.Count == 0);
        }

        [Fact]
        public async Task TestIsPermissionGrantedForUser()
        {
            var isPermissionGranted =
                await _permissionAppService.IsPermissionGrantedForUserAsync(ContextUser, ApiUserPermission);
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
            await KodkodInMemoryContext.SaveChangesAsync();

            var initializedPermission = KodkodInMemoryContext.Permissions.FirstOrDefault(p => p.Id == testPermission.Id);
            Assert.NotNull(initializedPermission);
        }
    }
}
