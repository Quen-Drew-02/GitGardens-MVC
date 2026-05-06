using System.ComponentModel.DataAnnotations;

namespace GitGardens.Models
{
    public class AddGardenMetric
    {
        public int GardenID { get; set; }

        [Range(0, 100)]
        public decimal Moisture { get; set; }

        [Range(0, 14)]
        public decimal PH { get; set; }

        [Range(-15, 50)]
        public decimal Temperature { get; set; }

        [Range(0, 100)]
        public decimal Humidity { get; set; }

        [Range(0, 100)]
        public decimal Sunlight { get; set; }

        [Range(0, 100)]
        public decimal Nitrogen { get; set; }
    }
}
