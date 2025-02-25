using IssueManagement.Domain.Models;
using IssueManagement.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IssueManagement.Persistence.Context
{
    public class IssueManagementContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public IssueManagementContext(DbContextOptions<IssueManagementContext> options) : base(options)
        {
        }

        public DbSet<Issue> issues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Issue>().ToTable("Issues");

            modelBuilder.Entity<Priority>().ToTable("Priorities");
            modelBuilder.Entity<Status>().ToTable("Statuses");

            modelBuilder.Ignore<IdentityUserToken<int>>();
            modelBuilder.Ignore<IdentityUserLogin<int>>();

            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");

            modelBuilder.ApplyConfiguration(new IssueConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            modelBuilder.ApplyConfiguration(new PriorityConfiguration());
        }
    }
}
