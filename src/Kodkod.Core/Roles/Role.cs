using System;
using System.Collections.Generic;
using Kodkod.Core.Entities;
using Kodkod.Core.Users;
using Microsoft.AspNetCore.Identity;

namespace Kodkod.Core.Roles
{
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
