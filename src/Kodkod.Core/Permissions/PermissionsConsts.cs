using System;
using System.Collections.Generic;

namespace Kodkod.Core.Permissions
{
    public class PermissionsConsts
    {
        public const string ApiUser = "ApiUser";

        private static readonly Permission ApiUserPermission = new Permission
        {
            DisplayName = "Api user",
            Name = ApiUser,
            Id = Guid.NewGuid()
        };

        public static List<Permission> AllPermissions()
        {
            return new List<Permission>
            {
                ApiUserPermission
            };
        }
    }
}
