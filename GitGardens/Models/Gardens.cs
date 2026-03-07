using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGardens.Models
{
    [Table("Gardens")]
    public class Gardens
    {
        public int GardenId { get; set; }  // Primary Key

        [Required]
        public string UserId { get; set; }           // Foreign key


        [Required]
        [MaxLength(150)]
        public string GardenName { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }           // Optional Description

        // Timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation property representing all metrics for Each respective Garden
        public ICollection<GardenMetrics> GardenMetrics { get; set; }
    }
}
