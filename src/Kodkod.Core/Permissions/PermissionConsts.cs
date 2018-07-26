using System;
using System.Collections.Generic;

namespace Kodkod.Core.Permissions
{
    public class PermissionConsts
    {
        public const string Name_ApiAccess = "ApiUser";
        public const string Name_AdminAccess = "Admin";
        public const string Name_UserList = "UserList";

        private static readonly Permission Permission_ApiAccess = new Permission
        {
            DisplayName = "Api access",
            Name = Name_ApiAccess,
            Id = new Guid("28126FFD-51C2-4201-939C-B64E3DF43B9D")
        };

        private static readonly Permission Permission_AdminAccess = new Permission
        {
            DisplayName = "Admin access",
            Name = Name_AdminAccess,
            Id = new Guid("28126FFD-51C2-4201-939C-B64E3DF43B9D")
        };

        private static readonly Permission Permission_UserList = new Permission
        {
            DisplayName = "User list",
            Name = Name_UserList,
            Id = new Guid("86D804BD-D022-49A5-821A-D2240478AAC4")
        };

        public static List<Permission> AllPermissions()
        {
            return new List<Permission>
            {
                Permission_ApiAccess,
                Permission_AdminAccess,
                Permission_UserList
            };
        }
    }
}
