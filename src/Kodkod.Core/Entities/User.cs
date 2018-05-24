using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Kodkod.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}