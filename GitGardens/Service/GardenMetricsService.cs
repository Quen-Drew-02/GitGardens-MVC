using GitGardens.Interface;
using GitGardens.Models;

namespace GitGardens.Service
{

    /*
    Title: ASP.NET Core MVC CRUD - NET 6 MVC CRUD  
    Operations Using Entity Framework Core and SQL Server  
    Author: Sameer Saini  
    Date: 2 years ago
    Code version : 1  
    Date Accessed : 27/03/26
    Availability: https://youtu.be/2Cp8Ti_f9Gk?feature=shared
    */

    /*
    Title: SOLID Principles: Do You Really Understand Them?    
    Author: Alex Hyett
    Date: 2 years ago
    Code version : 1  
    Date Accessed : 27/03/26
    Availability: https://youtu.be/kF7rQmSRlq0?si=6xaejm0UR_Hx2qHZ
    */

    // Handle Garden Metric Creation Logic and Ownership Validation
    public class GardenMetricsService : IGardenMetricsService
    {
        private readonly IGardenMetricsRepository _metricsRepository;
        private readonly IGardenRepository _gardenRepository;

        public GardenMetricsService(IGardenMetricsRepository metricsRepository, IGardenRepository gardenRepository)
        {
            _metricsRepository = metricsRepository;
            _gardenRepository = gardenRepository;
        }

        public async Task<bool> AddMetricAsync(int gardenID, int userID,
            decimal moisture, decimal ph, decimal temperature,
            decimal humidity, decimal sunlight, decimal nitrogen)
        {
            var garden = await _gardenRepository.GetGardenByIDAsync(gardenID);

            // Ownership Validation
            if (garden == null || garden.UserId != userID)
            {
                return false;
            }

            var metrics = new GardenMetrics
            {
                GardenID = gardenID,
                Moisture = moisture,
                PH = ph,
                Temperature = temperature,
                Humidity = humidity,
                Sunlight = sunlight,
                Nitrogen = nitrogen,
                RecordedAt = DateTime.UtcNow
            };

            await _metricsRepository.AddMetricAsync(metrics);
            await _metricsRepository.SaveAsync();

            return true;

        }

    }
}
