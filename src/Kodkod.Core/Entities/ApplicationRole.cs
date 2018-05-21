using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Kodkod.Core.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
