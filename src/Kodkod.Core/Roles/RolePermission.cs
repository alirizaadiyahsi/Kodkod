using System;
using Kodkod.Core.Entities;

namespace Kodkod.Core.Roles
{
    public class RolePermission
    {
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }

        public Guid PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
    }
}