using System;
using System.Collections.Generic;

namespace Kodkod.Core.Permissions
{
    //todo: code smell: write more generic
    public class PermissionConsts
    {
        public const string ApiUser = "ApiUser";

        private static readonly Permission ApiUserPermission = new Permission
        {
            DisplayName = "Api user",
            Name = ApiUser,
            Id = new Guid("28126FFD-51C2-4201-939C-B64E3DF43B9D")
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
