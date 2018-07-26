using System;
using System.Collections.Generic;

namespace Kodkod.Core.Permissions
{
    public class PermissionConsts
    {
        public const string ApiUser_Name = "ApiUser";
        public const string Admin_UserList_Name = "Admin.UserList";

        private static readonly Permission ApiUser_Permission = new Permission
        {
            DisplayName = "Api user",
            Name = ApiUser_Name,
            Id = new Guid("28126FFD-51C2-4201-939C-B64E3DF43B9D")
        };

        private static readonly Permission Admin_UserList_Permission = new Permission
        {
            DisplayName = "Admin user list",
            Name = Admin_UserList_Name,
            Id = new Guid("86D804BD-D022-49A5-821A-D2240478AAC4")
        };

        public static List<Permission> AllPermissions()
        {
            return new List<Permission>
            {
                ApiUser_Permission,
                Admin_UserList_Permission
            };
        }
    }
}
