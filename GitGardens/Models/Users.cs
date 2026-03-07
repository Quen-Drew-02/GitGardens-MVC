using System.ComponentModel.DataAnnotations.Schema;

namespace GitGardens.Models
{
    [Table("Users")]
    public class User
    {
        public int UserID { get; set; }   // Primary Key

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }   // Hased Password

        // Foriegn Key
        public int RoleID { get; set; }

        public Role Role { get; set; }

        // Navigation property representing Gardens Managed by a User
        public ICollection<Gardens> Gardens { get; set; }

    }
}
