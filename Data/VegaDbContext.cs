namespace Vega.Data
{
    using Microsoft.EntityFrameworkCore;
    using Vega.Data.Models;

    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Feature> Features { get; set; }
    }
}