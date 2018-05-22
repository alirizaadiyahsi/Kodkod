using System.Threading.Tasks;
using Kodkod.Application.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Kodkod.Web.Api.Authentication
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionApplicationService _permissionApplication;

        public PermissionHandler(IPermissionApplicationService permissionApplication)
        {
            _permissionApplication = permissionApplication;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                context.Fail();
            }

            var hasPermission = _permissionApplication.CheckPermissionForUser(context.User, requirement.Permission);
            if (hasPermission)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}