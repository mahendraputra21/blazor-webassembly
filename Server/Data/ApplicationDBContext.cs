using Blazor.Learner.Shared.Domains;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Learner.Server.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Developer> Developers { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Developer>(entity =>
            {
                // Set key for entity
                entity.HasKey(p => p.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
