using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Kodkod.Core.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
