using IndependentTree.Application.Interfaces;
using IndependentTree.Domain;
using IndependentTree.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Node> Node { get; set; }
        public DbSet<Tree> Tree { get; set; }
        public DbSet<Journal> Journal { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TreeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
