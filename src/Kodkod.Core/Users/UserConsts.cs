using System;
using System.Globalization;

namespace Kodkod.Core.Users
{
    public class UserConsts
    {
        public const string AdminUserName = "admin";
        public static string AdminEmail = "admin@mail.com";
        public static string PasswordHashFor123Qwe = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw=="; //123qwe

        public const string ApiUserName = "apiuser";
        public static string ApiEmail = "apiuser@mail.com";

        public static readonly User AdminUser = new User
        {
            Id = new Guid("C41A7761-6645-4E2C-B99D-F9E767B9AC77"),
            UserName = AdminUserName,
            Email = AdminEmail,
            EmailConfirmed = true,
            NormalizedEmail = AdminEmail.ToUpper(CultureInfo.InvariantCulture),
            NormalizedUserName = AdminUserName.ToUpper(CultureInfo.InvariantCulture),
            AccessFailedCount = 5,
            PasswordHash = PasswordHashFor123Qwe
        };

        public static readonly User ApiUser = new User
        {
            Id = new Guid("065E903E-6F7B-42B8-B807-0C4197F9D1BC"),
            UserName = ApiUserName,
            Email = ApiEmail,
            EmailConfirmed = true,
            NormalizedEmail = ApiEmail.ToUpper(CultureInfo.InvariantCulture),
            NormalizedUserName = ApiUserName.ToUpper(CultureInfo.InvariantCulture),
            AccessFailedCount = 5,
            PasswordHash = PasswordHashFor123Qwe
        };
    }
}
