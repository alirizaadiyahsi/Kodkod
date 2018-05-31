using System;
using System.Collections.Generic;
using Kodkod.Core.Permissions;
using Kodkod.Core.Roles;
using Kodkod.Core.Users;
using Kodkod.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Kodkod.Tests.Shared
{
    public class TestBase
    {
        public static readonly User AdminUser = new User
        {
            Id = new Guid("C41A7761-6645-4E2C-B99D-F9E767B9AC77"),
            UserName = "admin"
        };

        public static readonly User TestUser = new User
        {
            Id = new Guid("065E903E-6F7B-42B8-B807-0C4197F9D1BC"),
            UserName = "testuser"
        };

        public static readonly Role AdminRole = new Role
        {
            Id = new Guid("F22BCE18-06EC-474A-B9AF-A9DE2A7B8263"),
            Name = "Admin"
        };

        public static readonly Role MemberRole = new Role
        {
            Id = new Guid("11D14A89-3A93-4D39-A94F-82B823F0D4CE"),
            Name = "Member"
        };

        public static readonly Permission ApiUserPermission = new Permission
        {
            Id = new Guid("41F04B93-8C0E-4AC2-B6BA-63C052A2F02A"),
            Name = "ApiUser"
        };

        public static readonly UserRole AdminUserRole = new UserRole
        {
            RoleId = AdminRole.Id,
            UserId = AdminUser.Id
        };

        public static readonly UserRole TestUserRole = new UserRole
        {
            RoleId = MemberRole.Id,
            UserId = TestUser.Id
        };

        public static readonly RolePermission AdminRolePermission = new RolePermission
        {
            PermissionId = ApiUserPermission.Id,
            RoleId = AdminRole.Id
        };

        public static readonly RolePermission MemberRolePermission = new RolePermission
        {
            PermissionId = ApiUserPermission.Id,
            RoleId = MemberRole.Id
        };

        public static readonly List<User> AllTestUsers = new List<User>
        {
            new User {UserName = "A"},
            new User {UserName = "B"},
            new User {UserName = "C"},
            new User {UserName = "D"},
            AdminUser,
            TestUser
        };

        public static KodkodDbContext GetEmptyDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<KodkodDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            optionsBuilder.UseLazyLoadingProxies();

            var inMemoryContext = new KodkodDbContext(optionsBuilder.Options);

            return inMemoryContext;
        }

        public static KodkodDbContext GetInitializedDbContext()
        {
            var inMemoryContext = GetEmptyDbContext();

            inMemoryContext.AddRange(ApiUserPermission);
            inMemoryContext.AddRange(AdminRole, MemberRole);
            inMemoryContext.AddRange(AllTestUsers);
            inMemoryContext.AddRange(AdminUserRole, TestUserRole);
            inMemoryContext.AddRange(AdminRolePermission, MemberRolePermission);

            inMemoryContext.SaveChanges();

            return inMemoryContext;
        }
    }
}
