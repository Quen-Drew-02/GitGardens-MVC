using System.Collections.Generic;
using System.Linq;
using GitGardens.Models;

namespace GitGardens.Services
{
    /// <summary>
    /// Generates actionable tips based on submitted garden metrics.
    /// Call `MetricTipsService.GenerateTips(metrics)` after creating/saving a `GardenMetrics`.
    /// </summary>
    public static class MetricTipsService
    {
        public static List<string> GenerateTips(GardenMetrics m)
        {
            var tips = new List<string>();
            if (m == null) return tips;

            // pH guidance
            if (m.PH < 5.5m)
                tips.Add("Soil pH is low. Consider adding lime or wood ash to raise pH gradually.");
            else if (m.PH >= 5.5m && m.PH < 6.5m)
                tips.Add("Soil pH is slightly acidic; many vegetables do well in pH 6.0–7.0.");
            else if (m.PH > 7.5m)
                tips.Add("Soil pH is high. Elemental sulfur or acidic organic matter can help lower pH.");

            // Moisture
            if (m.Moisture < 25m)
                tips.Add("Soil moisture is low. Water deeply and less frequently to encourage strong roots.");
            else if (m.Moisture > 80m)
                tips.Add("Soil moisture is high. Improve drainage and avoid overwatering to prevent root rot.");

            // Temperature (Celsius)
            if (m.Temperature < 10m)
                tips.Add("Temperature is low. Protect sensitive plants from frost and consider covers.");
            else if (m.Temperature > 35m)
                tips.Add("Temperature is high. Provide shade or extra irrigation during heat spells.");

            // Humidity
            if (m.Humidity > 85m)
                tips.Add("Air humidity is high. Ensure good airflow to reduce fungal disease risk.");

            // Sunlight (hours)
            if (m.Sunlight < 4m)
                tips.Add("Sunlight is low. Move plants to a brighter spot or prune overhead shade.");
            else if (m.Sunlight > 10m)
                tips.Add("Sunlight is intense. Provide afternoon shade for sensitive plants.");

            // Nitrogen
            if (m.Nitrogen < 5m)
                tips.Add("Nitrogen level appears low. Apply a balanced or nitrogen-rich fertilizer as needed.");
            else if (m.Nitrogen > 40m)
                tips.Add("Nitrogen level appears high. Reduce fertilizer input to avoid excessive vegetative growth.");

            if (!tips.Any())
                tips.Add("All measured metrics are within common acceptable ranges. Continue regular monitoring.");

            return tips;
        }
    }
}
