using System.ComponentModel.DataAnnotations;

namespace GitGardens.Models
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "Full Name is Required")]
        [StringLength(150)]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; } // Kept for display/identification

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; } // Nullable, so it's optional
    }
}
