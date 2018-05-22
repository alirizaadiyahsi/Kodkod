using System.Security.Claims;
using Kodkod.Core.Entities;
using Kodkod.EntityFramework;

namespace Kodkod.Application.Permissions
{
    public class PermissionApplicationService : IPermissionApplicationService
    {
        private readonly IRepository<Permission> _permissionRepository;
        private readonly IRepository<ApplicationUser> _userRepository;

        public PermissionApplicationService(
            IRepository<Permission> permissionRepository, 
            IRepository<ApplicationUser> userRepository)
        {
            _permissionRepository = permissionRepository;
            _userRepository = userRepository;
        }

        public bool CheckPermissionForUser(ClaimsPrincipal contextUser, Permission requirementPermission)
        {
            var user = _userRepository.GetFirstOrDefaultAsync(u => u.UserName == contextUser.Identity.Name);
            //todo: check user has permission
            return true;
        }
    }
}