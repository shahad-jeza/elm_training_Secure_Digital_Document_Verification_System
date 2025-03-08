namespace DocumentVerificationApi.Models
{
    public class Document
    {
        public int Id { get; set; } // Primary key
        public string Title { get; set; }
        public string FilePath { get; set; } // Path to the uploaded file
        public string VerificationCode { get; set; } // Unique code for verification
        public string Status { get; set; } // e.g., Pending, Verified, Rejected
        public DateTime CreatedAt { get; set; } // Timestamp of document creation

        // Foreign key for the User
        public int UserId { get; set; }
        public User User { get; set; } // Navigation property for User

        // Navigation property for the one-to-many relationship with VerificationLog
        public ICollection<VerificationLog> VerificationLogs { get; set; }
    }
}