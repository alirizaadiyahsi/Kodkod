using System.Security.Claims;
using System.Threading.Tasks;
using Kodkod.Core.Permissions;

namespace Kodkod.Application.Permissions
{
    public interface IPermissionAppService
    {
        Task<bool> IsPermissionGrantedForUserAsync(ClaimsPrincipal contextUser, Permission requirementPermission);
    }
}
