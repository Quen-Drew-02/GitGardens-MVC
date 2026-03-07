using System.ComponentModel.DataAnnotations;

namespace GitGardens.Models
{
    /*
       Title:Pro C# 10 with .NET 6 Foundational Principles and Practices in Programming
       Authors: Andrew Troelsen & Phil Japikse
       Date Accessed: 05 MArch 2026
       Code version : 1 
       Edition: 11th
       Availability: Apress
       */

    public class Register
    {
        [Required(ErrorMessage = "Full Name is Required")]
        [StringLength(150, ErrorMessage = "Username must be under 150 Characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 Characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        public int RoleID { get; set; }

    }
}
