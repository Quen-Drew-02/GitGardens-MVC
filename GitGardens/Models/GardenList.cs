namespace GitGardens.Models
{
    public class GardenList
    {
        public int GardenID { get; set; }
        public string GardenName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int HealthScore { get; set; }    //added for the calculated health score of the garden

    }
}
