using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Kodkod.Application.Permissions.Dto;
using Kodkod.Core.Permissions;
using Kodkod.Utilities.PagedList;

namespace Kodkod.Application.Permissions
{
    public interface IPermissionAppService
    {
        Task<IPagedList<PermissionListDto>> GetPermissionsAsync(GetPermissionsInput input);

        Task<bool> IsPermissionGrantedForUserAsync(ClaimsPrincipal contextUser, Permission requirementPermission);

        Task InitializePermissions(List<Permission> permissions);
    }
}
