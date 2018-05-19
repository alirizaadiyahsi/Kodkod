using System;
using Kodkod.Core;
using Kodkod.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kodkod.EntityFramework
{
    public class KodkodDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public KodkodDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("User").HasData(
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    Email = "admin@mail.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "ADMIN@MAIL.COM",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                },
                new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = "testuser",
                    Email = "testuser@mail.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "TESTUSER@MAIL.COM",
                    NormalizedUserName = "TESTUSER",
                    PasswordHash = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                });

            modelBuilder.Entity<ApplicationRole>().ToTable("Role").HasData(new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRole");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<ApplicationRoleClaim>().ToTable("RoleClaim");
            modelBuilder.Entity<ApplicationUserToken>().ToTable("UserToken");
        }
    }
}
