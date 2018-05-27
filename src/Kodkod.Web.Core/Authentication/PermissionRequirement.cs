using Kodkod.Core.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Kodkod.Web.Core.Authentication
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(Permission permission)
        {
            Permission = permission;
        }

        public Permission Permission { get; }
    }
}
