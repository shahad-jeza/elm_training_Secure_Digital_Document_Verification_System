using Microsoft.EntityFrameworkCore;
using DocumentVerificationApi.Models;

namespace DocumentVerificationApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<VerificationLog> VerificationLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship between User and Document
            modelBuilder.Entity<Document>()
                .HasOne(d => d.User) // A document belongs to one user
                .WithMany(u => u.Documents) // A user can have many documents
                .HasForeignKey(d => d.UserId) // Foreign key in Document
                .OnDelete(DeleteBehavior.Cascade); // Delete documents when a user is deleted

            // Configure the one-to-many relationship between Document and VerificationLog
            modelBuilder.Entity<VerificationLog>()
                .HasOne(vl => vl.Document) // A verification log belongs to one document
                .WithMany(d => d.VerificationLogs) // A document can have many verification logs
                .HasForeignKey(vl => vl.DocumentId) // Foreign key in VerificationLog
                .OnDelete(DeleteBehavior.Cascade); // Delete logs when a document is deleted

            // Configure the one-to-many relationship between User and VerificationLog (verifier)
            modelBuilder.Entity<VerificationLog>()
                .HasOne(vl => vl.VerifiedByUser) // A verification log is created by one user (verifier)
                .WithMany() // A user can verify many documents
                .HasForeignKey(vl => vl.VerifiedByUserId) // Foreign key in VerificationLog
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of a user if they have verification logs
        }
    }
}