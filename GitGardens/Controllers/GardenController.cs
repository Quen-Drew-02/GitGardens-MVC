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
        private readonly IWeatherApiService _weatherApiService;

        public GardenController(IGardenService gardenService, IWeatherApiService weatherService)
        {
        _gardenService = gardenService;
            _weatherApiService = weatherService;
        }

        ////////////////////////////////////////////////////////////////Create A Garden//////////////////////////////////////////////////////

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

        ////////////////////////////////////////////////////////////////Read List of User Gardens//////////////////////////////////////////////////////

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
                CreatedAt = g.CreatedAt,
                // No 'await' needed here now!
                HealthScore = g.GardenMetrics != null && g.GardenMetrics.Any()
            ? _gardenService.CalculateHealthScore(g.GardenMetrics.OrderByDescending(m => m.RecordedAt).First())
            : 0
            }).ToList();

            return View(model);

        }


        ////////////////////////////////////////////////////////////////Edit Garden Details//////////////////////////////////////////////////////

        // Pull in Garden Details
        public async Task<IActionResult> EditGarden(int id)
        {
            var userIDClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIDClaim == null)
            {
                return Unauthorized();
            }

            int userID = int.Parse(userIDClaim.Value);

            var garden = await _gardenService.GetGardenForEditAsync(id, userID);

            if(garden == null)
            {
                return NotFound();
            }

            var model = new EditGarden
            {
                GardenID = garden.GardenId,
                GardenName = garden.GardenName,
                Description = garden.Description,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGarden(EditGarden model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var userIDClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIDClaim == null)
            {
                return Unauthorized();
            }

            int userID = int.Parse(userIDClaim.Value);

            var success = await _gardenService.UpdateGardenAsync(model.GardenID, model.GardenName, model.Description, userID);

            if (!success)
            {
                return Unauthorized();
            }

            return RedirectToAction("GardenList", "Garden");

        }
     
        public async Task<IActionResult> Details(int id)
        {
            //e.g. to weather for durban north when viewing a garden
            var weather = await _weatherApiService.GetWeatherAsync("Durban North");
            ViewBag.Weather = weather;

            return View();
        }



        ////////////////////////////////////////////////////////////////Delete Garden Details//////////////////////////////////////////////////////
        public async Task<IActionResult> DeleteGarden(int id)
        {
            var userIDClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIDClaim == null)
            {
                return Unauthorized();
            }

            int userID = int.Parse(userIDClaim.Value);

            //Verify Ownership and Pull in Garden Details for Confirmation
            var garden = await _gardenService.GetGardenForEditAsync(id, userID);

            if (garden == null)
            {
                return NotFound();
            }

            // Creating a new instance of the DeleteGarden model to pass to the view
            var model = new DeleteGarden
            {
                GardenId = garden.GardenId,
                GardenName = garden.GardenName,
                Description = garden.Description
            };

            return View(model);
        }

        [HttpPost, ActionName("DeleteGarden")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int GardenId)
        {
            var userIDClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            // Ensure User is Authenticated (logged in) before attempting deletion
            if (userIDClaim == null)
            {
                return Unauthorized();
            }

            int userID = int.Parse(userIDClaim.Value);

            var success = await _gardenService.DeleteGardenAsync(GardenId, userID);

            // Ownership Validation and Deletion Result Check
            if (!success)
            { 
                return Unauthorized();
            }

            return RedirectToAction("GardenList", "Garden");
        }
    }
}
