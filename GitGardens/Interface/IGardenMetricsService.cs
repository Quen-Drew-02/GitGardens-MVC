using GitGardens.Models;

namespace GitGardens.Interface
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

    public interface IGardenMetricsService
    {
        // Fetch the values of the latest metrics for a specific garden
        Task<GardenMetrics?> GetLatestMetricsAsync(int gardenID);

        Task<bool> AddMetricAsync(int gardenID, int userID,
            decimal moisture, 
            decimal ph, 
            decimal temperature,
            decimal humidity, 
            decimal sunlight, 
            decimal nitrogen);
    }
}
