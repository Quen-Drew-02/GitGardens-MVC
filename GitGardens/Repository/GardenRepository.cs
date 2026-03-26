using GitGardens.Data;
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

namespace GitGardens.Repository
{
    // Takes care of Database Operations
    public class GardenRepository : IGardenRepository
    {
        private readonly GitGardensDBContext _context;

        public GardenRepository(GitGardensDBContext context)
        {
            _context = context;
        }

        // Add New Garden
        public async Task AddGardenAsync(Gardens garden)
        {
            await _context.Gardens.AddAsync(garden);
        }

        // Refelect Changes
        public async Task SaveAsync()
        {
        await _context.SaveChangesAsync();
        }

    }
}
