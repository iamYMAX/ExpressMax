using System.ComponentModel.DataAnnotations;

namespace YourAppName.Web.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The Email / Username field is required.")]
        [Display(Name = "Email / Username")]
        // While the property is named Username, users might input their email.
        // ASP.NET Core Identity can typically handle finding users by username or email.
        // If strictly username, remove [EmailAddress] or use a custom validation.
        // For now, let's assume username could be an email.
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
