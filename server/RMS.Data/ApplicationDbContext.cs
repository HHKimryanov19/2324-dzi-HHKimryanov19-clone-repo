using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RMS.Data.Models;
using RMS.Shared.Contracts;
using System.Reflection.Emit;

namespace RMS.Data
{
    /// <summary>
    /// Class representing all tables in database.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly EntityState[] auditableStates =
        {
            EntityState.Added,
            EntityState.Modified,
        };

        private readonly ICurrentUser currentUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUser currentUser = default!)
            : base(options)
        {
            this.currentUser = currentUser;
        }

        /// <summary>
        /// DbSet representing Bookings table in database.
        /// </summary>
        public DbSet<Booking> Bookings { get; set; }

        /// <summary>
        /// DbSet representing Dishes table in database.
        /// </summary>
        public DbSet<Dish> Dishes { get; set; }

        /// <summary>
        /// DbSet representing Orders table in database.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// DbSet representing Restaurants table in database.
        /// </summary>
        public DbSet<Restaurant> Restaurants { get; set; }

        /// <summary>
        /// DbSet representing OrdersDishes table in database.
        /// </summary>
        public DbSet<OrdersDishes> OrdersDishes { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.HandleAuditableEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override int SaveChanges()
        {
            this.HandleAuditableEntities();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.HandleAuditableEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            this.HandleAuditableEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            builder.UseCollation("Cyrillic_General_CI_AS");
            base.OnModelCreating(builder);
        }

        private void HandleAuditableEntities()
        {
            var userId = this.currentUser?.Id?.ToString();
            var now = DateTime.UtcNow;
            var auditableEntries = this.ChangeTracker
                .Entries()
                .Where(x => x.Entity is IAuditableEntity && this.auditableStates.Contains(x.State))
                .ToList();

            foreach (var entry in auditableEntries)
            {
                var entity = entry.Entity as IAuditableEntity;
                entity.UpdateOn = now;
                entity.UpdateBy = userId;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = now;
                    entity.CreatedBy = userId;
                }
            }
        }
    }
}