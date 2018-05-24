using System.Security.Claims;
using System.Threading.Tasks;
using Kodkod.Core.Entities;

namespace Kodkod.Application.Permissions
{
    public interface IPermissionAppService
    {
        Task<bool> CheckPermissionForUserAsync(ClaimsPrincipal contextUser, Permission requirementPermission);
    }
}
