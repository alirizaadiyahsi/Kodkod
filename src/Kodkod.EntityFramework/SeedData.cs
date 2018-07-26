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
        #region private fiels
        private static readonly User AdminUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = UserConsts.AdminUserName,
            Email = UserConsts.AdminEmail,
            EmailConfirmed = true,
            NormalizedEmail = UserConsts.AdminEmail.ToUpper(CultureInfo.InvariantCulture),
            NormalizedUserName = UserConsts.AdminUserName.ToUpper(CultureInfo.InvariantCulture),
            AccessFailedCount = 5,
            PasswordHash = UserConsts.PasswordHashFor123Qwe
        };

        private static readonly User ApiUser = new User
        {
            Id = Guid.NewGuid(),
            UserName = UserConsts.ApiUserName,
            Email = UserConsts.ApiEmail,
            EmailConfirmed = true,
            NormalizedEmail = UserConsts.ApiEmail.ToUpper(CultureInfo.InvariantCulture),
            NormalizedUserName =UserConsts.ApiUserName.ToUpper(CultureInfo.InvariantCulture),
            AccessFailedCount = 5,
            PasswordHash = UserConsts.PasswordHashFor123Qwe
        };

        private static readonly Role AdminRole = new Role
        {
            Id = Guid.NewGuid(),
            Name = RoleConsts.AdminRoleName,
            NormalizedName = RoleConsts.AdminRoleName.ToUpper(CultureInfo.InvariantCulture)
        };

        private static readonly Role ApiUserRole = new Role
        {
            Id = Guid.NewGuid(),
            Name = RoleConsts.ApiUserRoleName,
            NormalizedName = RoleConsts.ApiUserRoleName.ToUpper(CultureInfo.InvariantCulture)
        };
        #endregion

        #region BuildData
        public static User[] BuildApplicationUsers()
        {
            return new[]
            {
                AdminUser,
                ApiUser
            };
        }

        public static Role[] BuildApplicationRoles()
        {
            return new[]
            {
                AdminRole,
                ApiUserRole
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
                    RoleId = ApiUserRole.Id,
                    UserId = ApiUser.Id
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
                .FirstOrDefault(p => p.Name == PermissionConsts.ApiUser_Name);

            if (apiUserPermission != null)
            {
                rolePermissions.Add(new RolePermission
                {
                    PermissionId = apiUserPermission.Id,
                    RoleId = ApiUserRole.Id
                });
            }

            return rolePermissions.ToArray();
        }
        #endregion
    }
}
