using System.Linq;
using Kodkod.Core.Permissions;
using Kodkod.Core.Roles;
using Kodkod.Core.Users;

namespace Kodkod.EntityFramework
{
    public class SeedData
    {
        #region BuildData
        public static User[] BuildApplicationUsers()
        {
            return new[]
            {
                UserConsts.AdminUser,
                UserConsts.ApiUser
            };
        }

        public static Role[] BuildApplicationRoles()
        {
            return new[]
            {
                RoleConsts.AdminRole,
                RoleConsts.ApiUserRole
            };
        }

        public static UserRole[] BuildApplicationUserRoles()
        {
            return new[]
            {
                new UserRole
                {
                    RoleId = RoleConsts.AdminRole.Id,
                    UserId = UserConsts.AdminUser.Id
                },
                new UserRole
                {
                    RoleId = RoleConsts.ApiUserRole.Id,
                    UserId = UserConsts.ApiUser.Id
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
                    RoleId = RoleConsts.AdminRole.Id
                }).ToList();

            var apiUserPermission = PermissionConsts.AllPermissions()
                .FirstOrDefault(p => p.Name == PermissionConsts.Name_ApiAccess);

            if (apiUserPermission != null)
            {
                rolePermissions.Add(new RolePermission
                {
                    PermissionId = apiUserPermission.Id,
                    RoleId = RoleConsts.ApiUserRole.Id
                });
            }

            return rolePermissions.ToArray();
        }
        #endregion
    }
}
