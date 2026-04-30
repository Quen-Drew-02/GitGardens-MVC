using GitGardens.Interface;
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

namespace GitGardens.Service
{
    // Manages Garden Business Logic
    public class GardenService : IGardenService
    {
        private readonly IGardenRepository _gardenRepository;

        public GardenService(IGardenRepository gardenRepository)
        {
            _gardenRepository = gardenRepository;
        }

        // Create a new Garden
        public async Task CreateGardenAsync(string gardenName, string description, int userID)
        {
            var garden = new Gardens
            {
                GardenName = gardenName,
                Description = description,
                UserId = userID,
                CreatedAt = DateTime.UtcNow
            };

            await _gardenRepository.AddGardenAsync(garden);
            await _gardenRepository.SaveAsync();
        }

        // Show User Gardens
        public async Task<List<Gardens>> GetUserGardensAsync(int userID)
        {
            return await _gardenRepository.GetGardensByUserIDAsync(userID);
        }

        // Pull In Existing User Garden Details
        public async Task<Gardens?> GetGardenForEditAsync(int gardenID, int userID)
        {
            var garden = await _gardenRepository.GetGardenByIDAsync(gardenID);

            // Ownership Validation
            if (garden == null || garden.UserId != userID)
            {
                return null;
            }

            return garden;

        }

        // Update Garden Details
        public async Task<bool> UpdateGardenAsync(int gardenID, string name, string description, int userID)
        {
            var garden = await _gardenRepository.GetGardenByIDAsync(gardenID);

            if (garden == null || garden.UserId != userID)
            {
                return false;
            }

            garden.GardenName = name;
            garden.Description = description;
            garden.UpdatedAt = DateTime.Now;

            await _gardenRepository.UpdateGardenAsync(garden);
            await _gardenRepository.SaveAsync();

            return true;

        }

        // Delete Garden
        public async Task<bool> DeleteGardenAsync(int gardenID, int userID)
        {
            var garden = await _gardenRepository.GetGardenByIDAsync(gardenID);

            // Ownership Validation
            if (garden == null || garden.UserId != userID)
            {
                return false;
            }

            // Delete Garden and Save Changes
            await _gardenRepository.DeleteGardenAsync(garden);
            await _gardenRepository.SaveAsync();

            return true;
        }

        public int CalculateHealthScore(GardenMetrics metrics)
        {
            if (metrics == null) return 0;

            double score = 0;

            //Target Moisture : 50%
            score += Math.Max(0, 20 - (double)Math.Abs(metrics.Moisture - 50));

            //Target PH : 6.5
            score += Math.Max(0, 20 - (double)Math.Abs(metrics.PH - 6.5m) * 10);

            //Target Temperature : 22°C
            score += Math.Max(0, 20 - (double)Math.Abs(metrics.Temperature - 22));

            // Targert nitrogen and humiditu
            score += 40; // Base score for other factors

            return (int)Math.Clamp(score, 0, 100);
        }
    }
}