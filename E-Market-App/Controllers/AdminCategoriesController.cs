using E_Market_App.Middlewares;
using EMarketApp.Core.Application.Interfaces.Services;
using EMarketApp.Core.Application.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Pokedex_App.Controllers
{
    public class AdminCategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ValidateUserSession _validateUserSession;

        public AdminCategoriesController(ICategoryService categoryService, ValidateUserSession validateUserSession)
        {
            _categoryService = categoryService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.Page = "adminCategories";
            return View("AdminCategories", await _categoryService.GetAllViewModel());
        }

        public IActionResult Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.Page = "adminCategories";
            return View("SaveCategory", new SaveCategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCategoryViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }

            await _categoryService.Add(vm);
            return RedirectToRoute(new { controller = "adminCategories", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.Page = "adminCategories";
            return View("SaveCategory", await _categoryService.GetSaveViewModelById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveCategoryViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }

            await _categoryService.Update(vm);
            return RedirectToRoute(new { controller = "adminCategories", action = "Index" });
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.Page = "adminCategories";
            return View("DeleteCategory", await _categoryService.GetSaveViewModelById(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _categoryService.Delete(id);
            return RedirectToRoute(new { controller = "adminCategories", action = "Index" });
        }
    }
}
