using EMarketApp.Core.Application.Interfaces.Services;
using EMarketApp.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Pokedex_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdsService _adService;
        private readonly ICategoryService _categoryService;

        public HomeController(IAdsService adService, ICategoryService categoryService)
        {
            _adService = adService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(FilterViewModel vm)
        {
            ViewBag.CategoriesList = await _categoryService.GetAllViewModel();
            ViewBag.Page = "home";
            return View(await _adService.GetAllViewModelWithFilters(vm));
        }

        [HttpPost]
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Page = "adDetails";
            return View("AdDetails", await _adService.GetAdDetailsByIdAsync(id));
        }
    }
}