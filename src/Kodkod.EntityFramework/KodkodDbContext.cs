using System;
using Kodkod.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kodkod.EntityFramework
{
    public class KodkodDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public KodkodDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Permission>()
                .ToTable("Permission")
                .HasData(SeedData.BuildPermissions());

            modelBuilder.Entity((Action<EntityTypeBuilder<RolePermission>>)(b =>
            {
                b.ToTable("RolePermission");
                b.HasKey(rp => new { rp.RoleId, rp.PermissionId });
                b.HasOne(rp => rp.Role)
                    .WithMany(r => r.RolePermissions)
                    .HasForeignKey(pt => pt.RoleId);
                b.HasOne(rp => rp.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(rp => rp.PermissionId);
                b.HasData(SeedData.BuildRolePermissions());
            }));

            modelBuilder.Entity<ApplicationUser>().ToTable("User")
                .HasData(SeedData.BuildApplicationUsers());

            modelBuilder.Entity<ApplicationRole>().ToTable("Role")
                .HasData(SeedData.BuildApplicationRoles());

            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRole")
                .HasData(SeedData.BuildApplicationUserRoles());

            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<ApplicationRoleClaim>().ToTable("RoleClaim");
            modelBuilder.Entity<ApplicationUserToken>().ToTable("UserToken");
        }
    }
}
