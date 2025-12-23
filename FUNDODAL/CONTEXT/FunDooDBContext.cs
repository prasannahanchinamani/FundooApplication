using Microsoft.EntityFrameworkCore;
using ModelLayer.Enities;
using ModelLayer.Entities;

namespace DataAccessLayer.CONTEXT
{
    public class FunDooDBContext : DbContext
    {
        public FunDooDBContext(DbContextOptions<FunDooDBContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Notes>Notes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Notes>().ToTable("Notes");
            modelBuilder.Entity<Notes>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.UserId);
        }

    }
}
