using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGardens.Models
{
    // Roles - > Admin /User
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int RoleID { get; set; }    // Primary Key

        public string RoleName { get; set; }
    }
}
