using EMarketApp.Core.Application.Interfaces.Services;
using EMarketApp.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Pokedex_App.Controllers
{
    public class AdminAdsController : Controller
    {
        private readonly IAdsService _adService;
        private readonly ICategoryService _categoryService;

        public AdminAdsController(IAdsService adsService, ICategoryService categoryService)
        {
            _adService = adsService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Page = "adminAds";
            return View("adminAds",  await _adService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Page = "adminAds";
            return View("SaveAd", new SaveAdViewModel()
            {
                CategoriesList = await _categoryService.GetAllViewModel()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAdViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.CategoriesList = await _categoryService.GetAllViewModel();
                return View("SaveAd", vm);
            }

            await _adService.Add(vm);
            return RedirectToRoute(new { controller = "adminAds", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Page = "adminAds";
            return View("SaveAd", await _adService.GetSaveViewModelById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveAdViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.CategoriesList = await _categoryService.GetAllViewModel();
                return View("SaveAd", vm);
            }

            await _adService.Update(vm);
            return RedirectToRoute(new { controller = "adminAds", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Page = "adminAds";
            return View("DeleteAd", await _adService.GetSaveViewModelById(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _adService.Delete(id);
            return RedirectToRoute(new { controller = "adminAds", action = "Index" });
        }
    }
}
