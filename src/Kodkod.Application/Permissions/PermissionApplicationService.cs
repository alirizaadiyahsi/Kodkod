using System.Security.Claims;
using Kodkod.Core.Entities;

namespace Kodkod.Application.Permissions
{
    public class PermissionApplicationService : IPermissionApplicationService
    {
        public bool CheckPermissionForUser(ClaimsPrincipal contextUser, Permission requirementPermission)
        {
            //todo: implement this method
            throw new System.NotImplementedException();
        }
    }
}