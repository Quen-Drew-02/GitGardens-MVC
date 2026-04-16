using GitGardens.Interface;

namespace GitGardens.Service
{
    public class GardenTipsService : IGardenTipsService
    {
        private readonly IGardenMetricsService _metricsService;

        public GardenTipsService(IGardenMetricsService metricsService)
        {
            _metricsService = metricsService;
        }

        public async Task<List<string>> GetTipsAsync(int gardenId)
        {
            var metrics = await _metricsService.GetLatestMetricsAsync(gardenId);

            var tips = new List<string>();

            if (metrics == null)
            {
                tips.Add("No data available.");
                return tips;
            }

            // PH logic
            if (metrics.PH < 6)
                tips.Add("Soil is too acidic. Consider adding lime.");
            else if (metrics.PH > 8)
                tips.Add("Soil is too alkaline. Consider adding sulfur.");

            // Moisture
            if (metrics.Moisture < 30)
                tips.Add("Soil is too dry. Increase watering.");
            else if (metrics.Moisture > 80)
                tips.Add("Soil is too wet. Reduce watering.");

            // Temperature
            if (metrics.Temperature < 10)
                tips.Add("Temperature is too low. Consider protecting plants.");
            else if (metrics.Temperature > 35)
                tips.Add("Temperature is too high. Provide shade or water more frequently.");

            // Humidity
            if (metrics.Humidity < 30)
                tips.Add("Air is too dry. Consider misting plants.");
            else if (metrics.Humidity > 80)
                tips.Add("Humidity is too high. Ensure proper airflow.");

            // Nitrogen
            if (metrics.Nitrogen < 20)
                tips.Add("Nitrogen is low. Consider fertilizing.");
            else if (metrics.Nitrogen > 80)
                tips.Add("Too much nitrogen. Reduce fertilizer use.");

            return tips;
        }
    }
}
