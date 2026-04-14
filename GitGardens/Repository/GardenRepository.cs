using GitGardens.Data;
using GitGardens.Interface;
using GitGardens.Models;
using Microsoft.EntityFrameworkCore;

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

        // Retrieve User Gardens
        public async Task<List<Gardens>> GetGardensByUserIDAsync(int userID)
        {
            return await _context.Gardens
                .Where(g => g.UserId == userID)
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();
        }

        // Retreieve Garden Details
        public async Task<Gardens?> GetGardenByIDAsync(int gardenID)
        {
            return await _context.Gardens.FirstOrDefaultAsync(g => g.GardenId == gardenID);
        }

        // Update Garden Details
        public async Task UpdateGardenAsync(Gardens garden)
        {
            _context.Gardens.Update(garden);
        }

        // Delete Gardens from the Database
        public async Task DeleteGardenAsync(Gardens garden)
        {
            _context.Gardens.Remove(garden);
        }

    }
}
