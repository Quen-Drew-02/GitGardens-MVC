using GitGardens.Models;

/*
Title: ASP.NET Core MVC CRUD - NET 6 MVC CRUD  
Operations Using Entity Framework Core and SQL Server  
Author: Sameer Saini  
Date: 2 years ago
Code version : 1  
Date Accessed : 26/03/26
Availability: https://youtu.be/2Cp8Ti_f9Gk?feature=shared
*/

/*
Title: SOLID Principles: Do You Really Understand Them?    
Author: Alex Hyett
Date: 2 years ago
Code version : 1  
Date Accessed : 26/03/26
Availability: https://youtu.be/kF7rQmSRlq0?si=6xaejm0UR_Hx2qHZ
*/

namespace GitGardens.Interface
{
    public interface IGardenService
    {
        // Create a Garden
        Task CreateGardenAsync(string gardenName, string description, int userID);

        // Retreive List of Gardens
        Task<List<Gardens>> GetUserGardensAsync(int userID);

        // Edit Garden Details
        Task<Gardens?> GetGardenForEditAsync(int gardenID, int userID);
        Task<bool> UpdateGardenAsync(int gardenID, string name, string description, int userID);

        // Delete Gardens
        Task<bool> DeleteGardenAsync(int gardenID, int userID);

        //Health Score
        int CalculateHealthScoreAsync(GardenMetrics metrics);
    }
}
