using System;
using Kodkod.Core.Roles;
using Microsoft.AspNetCore.Identity;

namespace Kodkod.Core.Users
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
