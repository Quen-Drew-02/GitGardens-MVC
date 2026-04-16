using GitGardens.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GitGardens.Controllers
{
    public class GardenTipsController : Controller
    {
        private readonly IGardenTipsService _tipsService;

        public GardenTipsController(IGardenTipsService tipsService)
        {
            _tipsService = tipsService;
        }

        public async Task<IActionResult> GetTips(int gardenId)
        {
            var tips = await _tipsService.GetTipsAsync(gardenId);
            return View(tips);
        }
    }
}
