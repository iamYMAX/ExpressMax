namespace YourAppName.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        // Navigation property for the related Employee (for one-to-one)
        public virtual Employee? Employee { get; set; }
    }
}
