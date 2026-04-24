using GitGardens.Data;
using GitGardens.Interface;
using GitGardens.Models;
using Microsoft.EntityFrameworkCore;

namespace GitGardens.Repository
{
    // Handles ENvironmental Metric Writes to Database
    public class GardenMetricsRepository : IGardenMetricsRepository
    {
        private readonly GitGardensDBContext _context;

        public GardenMetricsRepository(GitGardensDBContext context)
        {
            _context = context;
        }

        // Add Metrics
        public async Task AddMetricAsync(GardenMetrics metrics)
        {
            await _context.AddAsync(metrics);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Get Latest Metrics for a Garden
        public async Task<GardenMetrics?> GetLatestMetricsAsync(int gardenID)
        {
            return await _context.GardenMetrics
                .Where(m => m.GardenID == gardenID)
                .OrderByDescending(m => m.RecordedAt)
                .FirstOrDefaultAsync();
        }


    }
}
