namespace Vega.Data
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Common.Models;
    using Models;

    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Feature> Features { get; set; }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(x => new { x.VehicleId, x.FeatureId });
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            var entires = this.ChangeTracker.Entries();
            var added = entires.Where(e => e.Entity is IAuditInfo && ((e.State == EntityState.Added)));
            var Modified = entires.Where(e => e.Entity is IAuditInfo && ((e.State == EntityState.Modified)));

            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(e => e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.UtcNow
                        .AddHours(3); // Sofia - UTC +3
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow
                        .AddHours(3); // Sofia - UTC +3
                }
            }
        }
    }
}