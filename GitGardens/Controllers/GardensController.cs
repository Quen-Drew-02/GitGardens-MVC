using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GitGardens.Data;
using GitGardens.Models;
using Microsoft.EntityFrameworkCore;

namespace GitGardens.Controllers
{
    public class GardensController : Controller
    {
        private readonly GitGardensDBContext _context;
        public GardensController(GitGardensDBContext context)
        {
            _context = context;
        }

        public IActionResult Details(int id)
        {
            var garden = _context.Gardens
                .Include(g => g.GardenMetrics)
                .FirstOrDefault(g => g.GardenId == id);

            if (garden == null) return NotFound();

            return View(garden);
        }
    }
}
