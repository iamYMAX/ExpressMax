using Microsoft.AspNetCore.Identity; // Required for IdentityUser

namespace YourAppName.Data.Models
{
    public class User : IdentityUser<int> // Inherit from IdentityUser with int as key type
    {
        // Id is inherited from IdentityUser<int>
        // UserName is inherited
        // PasswordHash is inherited
        // Email, PhoneNumber, etc., are also available from IdentityUser

        // This existing Role property might be used for application-specific roles
        // or managed via IdentityRole claims. Let's keep it for now.
        public string Role { get; set; }

        // Navigation property for the related Employee (for one-to-one)
        public virtual Employee? Employee { get; set; }
    }
}
