using Microsoft.EntityFrameworkCore;

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
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<NoteLabel> NoteLabels { get; set; }
        public DbSet<NoteCollaborator> NoteCollaborators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table names
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Notes>().ToTable("Notes");
            modelBuilder.Entity<Label>().ToTable("Labels");
            modelBuilder.Entity<NoteLabel>().ToTable("NoteLabels");

            modelBuilder.Entity<Notes>()
                   .HasOne<User>()
                   .WithMany()
                   .HasForeignKey(n => n.UserId);


            modelBuilder.Entity<Label>()
               .HasOne<User>()
               .WithMany()
               .HasForeignKey(l => l.UserId);



            modelBuilder.Entity<NoteLabel>()
        .HasKey(nl => new { nl.NotesId, nl.LabelId });

            modelBuilder.Entity<NoteLabel>()
                .HasOne<Notes>()
                .WithMany()
                .HasForeignKey(nl => nl.NotesId)
                .OnDelete(DeleteBehavior.Restrict);   

            modelBuilder.Entity<NoteLabel>()
                .HasOne<Label>()
                .WithMany()
                .HasForeignKey(nl => nl.LabelId);      
            modelBuilder.Entity<NoteCollaborator>()
                .HasIndex(c => new { c.NoteId, c.Email })
                .IsUnique();

         
            modelBuilder.Entity<NoteCollaborator>()
                .HasOne<Notes>()
                .WithMany()
                .HasForeignKey(c => c.NoteId)
                .OnDelete(DeleteBehavior.Cascade);//cascading when parent datas deleted child data also delete 

        }
    }
}
