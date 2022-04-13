using Blazor.Learner.Shared.Models;
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

            //modelBuilder.Entity<Developer>(entity =>
            //{
            //    entity.HasKey(e => e.PositionId);

            //    //entity.HasOne(d => d.Position)
            //    //    .WithMany(p => p.Developers)
            //    //    .HasForeignKey(d => d.PositionId)
            //    //    .HasConstraintName("DF__Developer__Posit__38996AB5");
            //});


            //modelBuilder.Entity<Position>(entity =>
            //{
            //    entity.Property(e => e.PositionName)
            //        .IsRequired()
            //        .HasMaxLength(50)
            //        .IsUnicode(false);
            //});
        }
    }
}
