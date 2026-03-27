using System.ComponentModel.DataAnnotations;

namespace GitGardens.Models
{
    public class EditGarden
    {
        public int GardenID { get; set; }

        [Required]
        [MaxLength(150)]
        public string GardenName { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
    }
}
