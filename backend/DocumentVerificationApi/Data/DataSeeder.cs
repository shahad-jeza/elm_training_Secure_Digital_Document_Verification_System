using DocumentVerificationApi.Models;
using System;
using System.Linq;

namespace DocumentVerificationApi.Data
{
    public static class DataSeeder
    {
        public static void Initialize(AppDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if there are already users in the database
            if (context.Users.Any())
            {
                return; // Database has already been seeded
            }

            // Seed Users
            var users = new[]
            {
                new User
                {
                    Name = "Admin User",
                    Email = "admin@example.com",
                    Password = "admin123", // In a real app, hash this password!
                    Role = "Admin"
                },
                new User
                {
                    Name = "Regular User",
                    Email = "user@example.com",
                    Password = "user123", // In a real app, hash this password!
                    Role = "User"
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            // Seed Documents
            var documents = new[]
            {
                new Document
                {
                    Title = "Document 1",
                    FilePath = "/documents/doc1.pdf",
                    VerificationCode = Guid.NewGuid().ToString(),
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    UserId = users[0].Id // Assign to Admin User
                },
                new Document
                {
                    Title = "Document 2",
                    FilePath = "/documents/doc2.pdf",
                    VerificationCode = Guid.NewGuid().ToString(),
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    UserId = users[1].Id // Assign to Regular User
                }
            };

            context.Documents.AddRange(documents);
            context.SaveChanges();

            // Seed Verification Logs (Optional)
            var verificationLogs = new[]
            {
                new VerificationLog
                {
                    Timestamp = DateTime.UtcNow,
                    Status = "Verified",
                    DocumentId = documents[0].Id,
                    VerifiedByUserId = users[0].Id // Verified by Admin User
                }
            };

            context.VerificationLogs.AddRange(verificationLogs);
            context.SaveChanges();
        }
    }
}