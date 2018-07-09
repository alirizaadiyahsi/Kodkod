﻿using System;
using System.Collections.Generic;

namespace Kodkod.Core.Permissions
{
    public class PermissionConsts
    {
        public const string ApiUserPermissionName = "ApiUser";

        private static readonly Permission ApiUserPermission = new Permission
        {
            DisplayName = "Api user",
            Name = ApiUserPermissionName,
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
