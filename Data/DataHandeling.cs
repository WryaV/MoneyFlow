using Domain;
using Microsoft.EntityFrameworkCore;

namespace dataHandeling
{
    public class DataHandeling : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Flow> Flows { get; set; }
        public DbSet<Total> Totals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the database connection to use a SQLite database
            optionsBuilder.UseSqlite(@"Data Source=H:\5. NET\Lexicon\LexProject\MoneyFlow\MoneyFlow.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set up a foreign key relationship between Flow and User
            modelBuilder.Entity<Flow>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId);
        }
    }
}
