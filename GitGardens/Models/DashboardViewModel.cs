namespace GitGardens.Models
{
    public class DashboardViewModel
    {
        public int TotalGardens { get; set; }
        public int AverageHealth { get; set; }
        public int NeedsAttentionCount { get; set; }
        public List<RecentGardenSummary> RecentGardens { get; set; } = new List<RecentGardenSummary>();
    }

    public class RecentGardenSummary
    {
        public int GardenID { get; set; }
        public string GardenName { get; set; }
        public int HealthScore { get; set; }
        public DateTime? LastRecordedAt { get; set; }
    }
}
