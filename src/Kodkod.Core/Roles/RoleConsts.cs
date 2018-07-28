using System;
using System.Globalization;

namespace Kodkod.Core.Roles
{
    public class RoleConsts
    {
        public const string AdminRoleName = "Admin";
        public static string ApiUserRoleName = "ApiUserRole";

        public static readonly Role AdminRole = new Role
        {
            Id = new Guid("F22BCE18-06EC-474A-B9AF-A9DE2A7B8263"),
            Name = AdminRoleName,
            NormalizedName = AdminRoleName.ToUpper(CultureInfo.InvariantCulture)
        };

        public static readonly Role ApiUserRole = new Role
        {
            Id = new Guid("11D14A89-3A93-4D39-A94F-82B823F0D4CE"),
            Name = ApiUserRoleName,
            NormalizedName = ApiUserRoleName.ToUpper(CultureInfo.InvariantCulture)
        };
    }
}
