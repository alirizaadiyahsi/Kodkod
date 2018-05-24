using System;
using System.Collections.Generic;
using Kodkod.Core.Entities;
using Kodkod.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Kodkod.Application.Tests
{
    public class TestBase
    {
        public static readonly User AdminUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = "AdminUser"
        };

        public static readonly Role AdminRole = new Role
        {
            Id = Guid.NewGuid(),
            Name = "Admin"
        };

        public static readonly Permission ApiUserPermission = new Permission
        {
            Id = Guid.NewGuid(),
            Name = "ApiUser"
        };

        public KodkodDbContext GetEmptyDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<KodkodDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

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
                new User {UserName = "D"},
                new User {UserName = "E"}
            };

            inMemoryContext.AddRange(ApiUserPermission);
            inMemoryContext.AddRange(AdminRole);
            inMemoryContext.AddRange(AdminUser);
            inMemoryContext.AddRange(otherTestUsers);

            inMemoryContext.UserRoles.Add(new UserRole { RoleId = AdminRole.Id, UserId = AdminUser.Id });
            inMemoryContext.RolePermissions.Add(new RolePermission { PermissionId = ApiUserPermission.Id, RoleId = AdminRole.Id });

            inMemoryContext.SaveChanges();

            return inMemoryContext;
        }
    }
}
