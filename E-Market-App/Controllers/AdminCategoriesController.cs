using EMarketApp.Core.Application.Interfaces.Services;
using EMarketApp.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Pokedex_App.Controllers
{
    public class AdminCategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public AdminCategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Page = "adminCategories";
            return View("AdminCategories", await _categoryService.GetAllViewModel());
        }

        public IActionResult Create()
        {
            ViewBag.Page = "adminCategories";
            return View("SaveCategory", new CategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }

            await _categoryService.Add(vm);
            return RedirectToRoute(new { controller = "adminCategories", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Page = "adminCategories";
            return View("SaveCategory", await _categoryService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }

            await _categoryService.Update(vm);
            return RedirectToRoute(new { controller = "adminCategories", action = "Index" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Page = "adminCategories";
            return View("DeleteCategory", await _categoryService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToRoute(new { controller = "adminCategories", action = "Index" });
        }
    }
}
