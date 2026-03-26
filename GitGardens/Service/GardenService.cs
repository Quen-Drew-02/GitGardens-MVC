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

    }
}
