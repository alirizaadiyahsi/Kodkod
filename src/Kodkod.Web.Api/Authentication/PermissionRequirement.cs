using Kodkod.Core.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Kodkod.Web.Api.Authentication
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
