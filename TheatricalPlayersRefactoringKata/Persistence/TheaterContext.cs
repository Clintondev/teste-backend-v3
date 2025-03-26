using Microsoft.EntityFrameworkCore;

namespace TheatricalPlayersRefactoringKata.Persistence
{
    public class TheaterContext : DbContext
    {
        public TheaterContext(DbContextOptions<TheaterContext> options)
            : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<Performance> Performances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>().HasKey(i => i.Id);
            modelBuilder.Entity<Play>().HasKey(p => p.Id);
            modelBuilder.Entity<Performance>().HasKey(perf => perf.Id);

  
            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Performances)
                .WithOne() 
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
