using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Kodkod.Application.Permissions.Dto;
using Kodkod.Core.Permissions;
using Kodkod.Core.Users;
using Kodkod.EntityFramework.Repositories;
using Kodkod.Utilities.PagedList;
using Kodkod.Utilities.PagedList.Extensions;
using Kodkod.Utilities.Extensions;
using Kodkod.Utilities.Linq.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Kodkod.Application.Permissions
{
    public class PermissionAppService : IPermissionAppService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Permission> _permissionRepository;

        public PermissionAppService(
            IRepository<User> userRepository,
            IRepository<Permission> permissionRepository)
        {
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<IPagedList<PermissionListDto>> GetPermissionsAsync(GetPermissionsInput input)
        {
            var query = _permissionRepository.GetAll(
                    !input.Filter.IsNullOrEmpty(),
                    predicate => predicate.Name.Contains(input.Filter) ||
                                 predicate.Name.Contains(input.Filter))
                .OrderBy(input.Sorting);

            var permissionsCount = await query.CountAsync();
            var permissions = query.PagedBy(input.PageSize, input.PageIndex).ToList();
            var permissionListDtos = Mapper.Map<List<PermissionListDto>>(permissions);

            return permissionListDtos.ToPagedList(permissionsCount, input.PageIndex, input.PageSize);
        }

        public async Task<bool> IsPermissionGrantedForUserAsync(ClaimsPrincipal contextUser, Permission requirementPermission)
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.UserName == contextUser.Identity.Name);
            if (user == null)
            {
                return false;
            }

            var grantedPermissions = user.UserRoles
                .Select(ur => ur.Role)
                .SelectMany(r => r.RolePermissions)
                .Select(rp => rp.Permission);

            return grantedPermissions.Any(p => p.Name == requirementPermission.Name);
        }

        public async Task InitializePermissions(List<Permission> permissions)
        {
            foreach (var permission in permissions)
            {
                var existingPermission = await _permissionRepository.GetFirstOrDefaultAsync(p => p.Name == permission.Name);

                if (existingPermission == null)
                {
                    await _permissionRepository.InsertAsync(permission);
                }
            }
        }
    }
}