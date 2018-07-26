using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Kodkod.Application.Permissions.Dto;
using Kodkod.Core.Permissions;
using Kodkod.Core.Roles;
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
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        private readonly IMapper _mapper;

        public PermissionAppService(
            IRepository<User> userRepository,
            IRepository<Permission> permissionRepository,
            IRepository<Role> roleRepository,
            IRepository<RolePermission> rolePermissionRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _mapper = mapper;
        }

        public async Task<IPagedList<PermissionListDto>> GetPermissionsAsync(GetPermissionsInput input)
        {
            var query = _permissionRepository.GetAll(
                    !input.Filter.IsNullOrEmpty(),
                    predicate => predicate.Name.Contains(input.Filter) ||
                                 predicate.DisplayName.Contains(input.Filter))
                .OrderBy(input.Sorting);

            var permissionsCount = await query.CountAsync();
            var permissions = query.PagedBy(input.PageSize, input.PageIndex).ToList();
            var permissionListDtos = _mapper.Map<List<PermissionListDto>>(permissions);

            return permissionListDtos.ToPagedList(permissionsCount);
        }

        public async Task<bool> IsPermissionGrantedForUserAsync(ClaimsPrincipal contextUser, Permission permission)
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

            return grantedPermissions.Any(p => p.Name == permission.Name);
        }

        public async Task<bool> IsPermissionGrantedForRoleAsync(Role role, Permission permission)
        {
            var existingRole = await _roleRepository.GetFirstOrDefaultAsync(r => r.Id == role.Id);
            if (existingRole == null)
            {
                return false;
            }

            var grantedPermissions = existingRole.RolePermissions
                .Select(rp => rp.Permission);

            return grantedPermissions.Any(p => p.Name == permission.Name);
        }

        public async Task InitializePermissions(List<Permission> permissions)
        {
            _permissionRepository.Delete(_permissionRepository.GetAll());
            foreach (var permission in permissions)
            {
                await _permissionRepository.InsertAsync(permission);

                var role = await _roleRepository.GetFirstOrDefaultAsync(r => r.Name == RoleConsts.AdminRoleName);
                var rolePermission = new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.Id
                };
                await _rolePermissionRepository.InsertAsync(rolePermission);
            }
        }
    }
}