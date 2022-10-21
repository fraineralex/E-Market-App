using E_Market_App.Middlewares;
using EMarketApp.Core.Application.Interfaces.Services;
using EMarketApp.Core.Application.ViewModels.Ads;
using Microsoft.AspNetCore.Mvc;

namespace Pokedex_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdsService _adService;
        private readonly ICategoryService _categoryService;
        private readonly ValidateUserSession _validateUserSession;

        public HomeController(IAdsService adService, ICategoryService categoryService, ValidateUserSession validateUserSession)
        {
            _adService = adService;
            _categoryService = categoryService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index(FilterViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.CategoriesList = await _categoryService.GetAllViewModel();
            ViewBag.Page = "home";
            return View(await _adService.GetAllViewModelWithFilters(vm));
        }

        [HttpPost]
        public async Task<IActionResult> Details(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.Page = "adDetails";
            return View("AdDetails", await _adService.GetAdDetailsByIdAsync(id));
        }
    }
}