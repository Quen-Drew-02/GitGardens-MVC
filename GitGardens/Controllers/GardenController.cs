using GitGardens.Interface;
using GitGardens.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GitGardens.Controllers
{
    // Model Binding and Validation
    public class GardenController : Controller
    {
        private readonly IGardenService _gardenService;

        public GardenController(IGardenService gardenService)
        {
        _gardenService = gardenService;
        }

        // Garden Screen View
        public IActionResult CreateGarden()
        {
            return View();
        }

        // Garden Creation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGarden(string gardenName, string description)
        {
            if (string.IsNullOrWhiteSpace(gardenName))
            {
                ModelState.AddModelError("GardenName", "Garden name is Required");
                return View();
            }

            // Get Currently Logged in User ID
            var userIDClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if(userIDClaim == null){
                return Unauthorized();
            }

            int userID = int.Parse(userIDClaim.Value);

            await _gardenService.CreateGardenAsync(gardenName, description, userID);

            return RedirectToAction("Index", "Home");

        }

        // List of Gardens
        public async Task<IActionResult> GardenList()
        {
            var userIDClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIDClaim == null)
            {
                return Unauthorized();
            }

            int userID = int.Parse(userIDClaim.Value);

            var gardens = await _gardenService.GetUserGardensAsync(userID);

            var model = gardens.Select(g => new GardenList
            {
                GardenID = g.GardenId,
                GardenName = g.GardenName,
                Description = g.Description,
                CreatedAt = g.CreatedAt
            }).ToList();

            return View(model);

        }

    }
}
