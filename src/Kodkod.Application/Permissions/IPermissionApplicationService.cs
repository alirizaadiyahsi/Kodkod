using System.Security.Claims;
using Kodkod.Core.Entities;

namespace Kodkod.Application.Permissions
{
    public interface IPermissionApplicationService
    {
        bool CheckPermissionForUser(ClaimsPrincipal contextUser, Permission requirementPermission);
    }
}
