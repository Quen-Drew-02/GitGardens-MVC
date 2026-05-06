using GitGardens.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GitGardens.Controllers
{
    public class GardenTipsController : Controller
    {
        private readonly IGardenTipsService _tipsService;

        // Controller for providing gardening tips based on latest metrics
        public GardenTipsController(IGardenTipsService tipsService)
        {
            _tipsService = tipsService;
        }

        // Async method to fetch tips for a specific garden and pass them to the view
        public async Task<IActionResult> GetTips(int gardenId)
        {
            var tips = await _tipsService.GetTipsAsync(gardenId);

            ViewBag.GardenId = gardenId;

            return View(tips);
        }
    }
}
