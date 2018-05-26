using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Kodkod.Core.Permissions;

namespace Kodkod.Application.Permissions
{
    public interface IPermissionAppService
    {
        Task<List<Permission>> GetAllAsync();

        Task<bool> IsPermissionGrantedForUserAsync(ClaimsPrincipal contextUser, Permission requirementPermission);
        
        Task InitializePermissions(List<Permission> permissions);
    }
}
