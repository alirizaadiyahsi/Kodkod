using System;
using System.Linq;
using Kodkod.Core.AppConsts;
using Kodkod.Core.Entities;

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
                AdminRole
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
                }
            };
        }

        public static Permission[] BuildPermissions()
        {
            return KodkodPermissions.AllPermissions().ToArray();
        }

        public static RolePermission[] BuildRolePermissions()
        {
            return KodkodPermissions.AllPermissions().Select(p =>
                new RolePermission
                {
                    PermissionId = p.Id,
                    RoleId = AdminRole.Id
                }).ToArray();
        }
        #endregion
    }
}
