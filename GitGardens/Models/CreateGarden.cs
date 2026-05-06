using System.ComponentModel.DataAnnotations;

namespace GitGardens.Models
{
    public class CreateGarden
    {
        [Required(ErrorMessage = "Garden name is Required")]
        [MaxLength(100)]
        public string GardenName { get; set; }

        [MaxLength(275)]
        public string? Description { get; set; }
    }
}