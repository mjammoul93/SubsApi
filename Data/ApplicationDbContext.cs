using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SubsApi.DTOs;
using SubsApi.Models;
using System.Reflection.Emit;

namespace SubsApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole,
        IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // public DbSet<AppUser> Users { get; set; }
        public DbSet<GymSubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<MembersSubsciptions> Subsciptions { get; set; }

        public DbSet<SubscriptionDetailsDTO> SubscriptionsDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
               .HasMany(ur => ur.UserRoles)
               .WithOne(u => u.Role)
               .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

            builder.Entity<SubscriptionDetailsDTO>().HasNoKey().ToView(null);
        }
    }
}
