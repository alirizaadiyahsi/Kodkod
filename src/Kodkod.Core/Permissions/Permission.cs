using System.Collections.Generic;
using Kodkod.Core.Entities;
using Kodkod.Core.Roles;

namespace Kodkod.Core.Permissions
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}