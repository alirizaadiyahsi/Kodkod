using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Kodkod.Application.Permissions.Dto;
using Kodkod.Core.Permissions;

namespace Kodkod.Application.Permissions
{
    public interface IPermissionAppService
    {
        Task<List<Permission>> GetPermissionsAsync(FilterPermissionsInput input);

        Task<bool> IsPermissionGrantedForUserAsync(ClaimsPrincipal contextUser, Permission requirementPermission);
        
        Task InitializePermissions(List<Permission> permissions);
    }
}
