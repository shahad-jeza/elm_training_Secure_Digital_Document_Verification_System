namespace DocumentVerificationApi.Models
{
    public class User
    {
        public int Id { get; set; } // Primary key
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // e.g., Admin, User

        // Navigation property for the one-to-many relationship with Document
        public ICollection<Document> Documents { get; set; }
    }
}