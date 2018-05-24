using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Kodkod.Core.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<RolePermission> RolePermissions { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
