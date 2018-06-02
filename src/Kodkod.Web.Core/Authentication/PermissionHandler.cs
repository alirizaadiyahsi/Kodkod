using System.Threading.Tasks;
using Kodkod.Application.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Kodkod.Web.Core.Authentication
{
    //todo: write test for class
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

            var hasPermission = await _permissionApp.IsPermissionGrantedForUserAsync(context.User, requirement.Permission);
            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }
    }
}