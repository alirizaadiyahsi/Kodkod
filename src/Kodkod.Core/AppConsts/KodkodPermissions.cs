using System;
using System.Collections.Generic;
using Kodkod.Core.Entities;

namespace Kodkod.Core.AppConsts
{
    public class KodkodPermissions
    {
        public const string ApiUser = "ApiUser";

        public static List<Permission> AllPermissions()
        {
            return new List<Permission>
            {
                new Permission
                {
                    DisplayName = "Api user",
                    Name = ApiUser,
                    Id = Guid.NewGuid()
                }
            };
        }
    }
}
