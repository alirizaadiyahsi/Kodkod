using System;
using System.Globalization;
using System.Linq;
using Kodkod.Core.Permissions;
using Kodkod.Core.Roles;
using Kodkod.Core.Users;

namespace Kodkod.EntityFramework
{
    public class SeedData
    {
        //todo: define a const for all static strings
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

        //todo: rename, test user to api user
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
            Name = RoleConsts.AdminRoleName,
            NormalizedName = RoleConsts.AdminRoleName.ToUpper(CultureInfo.InvariantCulture)
        };

        //todo: find a better name, i.e. api role or test role
        private static readonly Role MemberRole = new Role
        {
            Id = Guid.NewGuid(),
            Name = RoleConsts.MemberRoleName,
            NormalizedName = RoleConsts.MemberRoleName.ToUpper(CultureInfo.InvariantCulture)
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
                    UserId = TestUser.Id
                }
            };
        }

        public static Permission[] BuildPermissions()
        {
            return PermissionConsts.AllPermissions().ToArray();
        }

        public static RolePermission[] BuildRolePermissions()
        {
            var rolePermissions = PermissionConsts.AllPermissions().Select(p =>
                new RolePermission
                {
                    PermissionId = p.Id,
                    RoleId = AdminRole.Id
                }).ToList();

            var apiUserPermission = PermissionConsts.AllPermissions()
                .FirstOrDefault(p => p.Name == PermissionConsts.ApiUserPermissionName);

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
