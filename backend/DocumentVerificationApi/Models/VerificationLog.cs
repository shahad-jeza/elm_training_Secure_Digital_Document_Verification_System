namespace DocumentVerificationApi.Models
{
    public class VerificationLog
    {
        public int Id { get; set; } // Primary key
        public DateTime Timestamp { get; set; } // Timestamp of verification
        public string Status { get; set; } // e.g., Verified, Rejected

        // Foreign key for the Document
        public int DocumentId { get; set; }
        public Document Document { get; set; } // Navigation property for Document

        // Foreign key for the User (verifier)
        public int VerifiedByUserId { get; set; }
        public User VerifiedByUser { get; set; } // Navigation property for User (verifier)
    }
}