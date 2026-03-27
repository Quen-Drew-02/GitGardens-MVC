using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGardens.Models
{
    [Table("GardenMetrics")]
    public class GardenMetrics
    {
        [Key]
        public int MetricId { get; set; }               // Primary key

        [Required]
        public int GardenID { get; set; }            // Foreign key

        public decimal Moisture { get; set; }      // Soil moisture Percentage

        public decimal PH { get; set; }          // Soil pH level

        public decimal Temperature { get; set; }                // Temperature in Celsius

        public decimal Humidity { get; set; }             // Air humidity Percentage

        public decimal Sunlight { get; set; }                       // Sunlight exposure Hours

        public decimal Nitrogen { get; set; }             // Nitrogen Level

        // Timestamp
        public DateTime RecordedAt { get; set; }

        [ForeignKey("GardenID")]
        public Gardens Gardens { get; set; }                  // Navigation Property to Parent Garden

    }
}
