namespace DocumentVerificationApi.Models
{
    public class VerificationLogDto
    {
        public int DocumentId { get; set; }
        public int VerifiedByUserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; } // e.g., Verified, Rejected
    }
}