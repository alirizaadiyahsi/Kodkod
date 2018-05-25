using System;
using System.Linq;
using Kodkod.Core.Entities;
using Kodkod.Core.Permissions;
using Kodkod.Core.Roles;
using Kodkod.Core.Users;

namespace Kodkod.EntityFramework
{
    public class SeedData
    {
        #region private fiels
        private static readonly User AdminUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = "admin",
            Email = "admin@mail.com",
            EmailConfirmed = true,
            NormalizedEmail = "ADMIN@MAIL.COM",
            NormalizedUserName = "ADMIN",
            AccessFailedCount = 5,
            PasswordHash = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
        };

        private static readonly User TestUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = "testuser",
            Email = "testuser@mail.com",
            EmailConfirmed = true,
            NormalizedEmail = "TESTUSER@MAIL.COM",
            NormalizedUserName = "TESTUSER",
            AccessFailedCount = 5,
            PasswordHash = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
        };

        private static readonly Role AdminRole = new Role
        {
            Id = Guid.NewGuid(),
            Name = "Admin",
            NormalizedName = "ADMIN"
        };

        private static readonly Role MemberRole = new Role
        {
            Id = Guid.NewGuid(),
            Name = "Member",
            NormalizedName = "MEMBER"
        };
        #endregion

        #region BuildData
        public static User[] BuildApplicationUsers()
        {
            return new[]
            {
                AdminUser,
                TestUser
            };
        }

        public static Role[] BuildApplicationRoles()
        {
            return new[]
            {
                AdminRole,
                MemberRole
            };
        }

        public static UserRole[] BuildApplicationUserRoles()
        {
            return new[]
            {
                new UserRole
                {
                    RoleId = AdminRole.Id,
                    UserId = AdminUser.Id
                },
                new UserRole
                {
                    RoleId = MemberRole.Id,
                    UserId=TestUser.Id
                }
            };
        }

        public static Permission[] BuildPermissions()
        {
            return PermissionsConsts.AllPermissions().ToArray();
        }

        public static RolePermission[] BuildRolePermissions()
        {
            var rolePermissions = PermissionsConsts.AllPermissions().Select(p =>
                new RolePermission
                {
                    PermissionId = p.Id,
                    RoleId = AdminRole.Id
                }).ToList();

            var apiUserPermission = PermissionsConsts.AllPermissions()
                .FirstOrDefault(p => p.Name == PermissionsConsts.ApiUser);

            if (apiUserPermission != null)
            {
                rolePermissions.Add(new RolePermission
                {
                    PermissionId = apiUserPermission.Id,
                    RoleId = MemberRole.Id
                });
            }

            return rolePermissions.ToArray();
        }
        #endregion
    }
}
