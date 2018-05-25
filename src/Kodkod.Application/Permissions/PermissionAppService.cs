using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kodkod.Core.Entities;
using Kodkod.EntityFramework.Repositories;

namespace Kodkod.Application.Permissions
{
    public class PermissionAppService : IPermissionAppService
    {
        private readonly IRepository<User> _userRepository;

        public PermissionAppService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CheckPermissionForUserAsync(ClaimsPrincipal contextUser, Permission requirementPermission)
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.UserName == contextUser.Identity.Name);
            if (user==null)
            {
                return false;
            }
            
            var grantedPermissions = user.UserRoles
                .Select(ur => ur.Role)
                .SelectMany(r => r.RolePermissions)
                .Select(rp => rp.Permission);

            return grantedPermissions.Any(p => p.Name == requirementPermission.Name);
        }
    }
}