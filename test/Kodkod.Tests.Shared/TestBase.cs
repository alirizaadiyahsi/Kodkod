using System;
using System.Collections.Generic;
using Kodkod.Core.Entities;
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
            Id = Guid.NewGuid(),
            UserName = "admin"
        };

        public static readonly User TestUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = "testuser"
        };

        public static readonly Role AdminRole = new Role
        {
            Id = Guid.NewGuid(),
            Name = "Admin"
        };

        public static readonly Role MemberRole = new Role
        {
            Id = Guid.NewGuid(),
            Name = "Member"
        };

        public static readonly Permission ApiUserPermission = new Permission
        {
            Id = Guid.NewGuid(),
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

        public KodkodDbContext GetEmptyDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<KodkodDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            optionsBuilder.UseLazyLoadingProxies();

            var inMemoryContext = new KodkodDbContext(optionsBuilder.Options);

            return inMemoryContext;
        }

        public KodkodDbContext GetInitializedDbContext()
        {
            var inMemoryContext = GetEmptyDbContext();

            var otherTestUsers = new List<User>
            {
                new User {UserName = "A"},
                new User {UserName = "B"},
                new User {UserName = "C"},
                new User {UserName = "D"}
            };

            inMemoryContext.AddRange(ApiUserPermission);
            inMemoryContext.AddRange(AdminRole, MemberRole);
            inMemoryContext.AddRange(AdminUser, TestUser);
            inMemoryContext.AddRange(otherTestUsers);
            inMemoryContext.AddRange(AdminUserRole, TestUserRole);
            inMemoryContext.AddRange(AdminRolePermission, MemberRolePermission);

            inMemoryContext.SaveChanges();

            return inMemoryContext;
        }
    }
}
