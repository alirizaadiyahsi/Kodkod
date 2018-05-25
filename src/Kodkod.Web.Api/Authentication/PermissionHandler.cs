using System.Threading.Tasks;
using Kodkod.Application.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Kodkod.Web.Api.Authentication
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionAppService _permissionApp;

        public PermissionHandler(IPermissionAppService permissionApp)
        {
            _permissionApp = permissionApp;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                return;
            }

            var hasPermission = await _permissionApp.CheckPermissionForUserAsync(context.User, requirement.Permission);
            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }
    }
}