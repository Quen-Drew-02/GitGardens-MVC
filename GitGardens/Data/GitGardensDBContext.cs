using GitGardens.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GitGardens.Data
{
    public class GitGardensDBContext : DbContext
    {
        public GitGardensDBContext(DbContextOptions options) : base(options) { }

        // Tables
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Gardens> Gardens { get; set; }
        public DbSet<GardenMetrics> GardenMetrics { get; set; }

    }
}