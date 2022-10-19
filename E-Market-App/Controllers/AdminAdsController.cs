using E_Market_App.Middlewares;
using EMarketApp.Core.Application.Interfaces.Services;
using EMarketApp.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Pokedex_App.Controllers
{
    public class AdminAdsController : Controller
    {
        private readonly IAdsService _adService;
        private readonly ICategoryService _categoryService;
        private readonly ValidateUserSession _validateUserSession;

        public AdminAdsController(IAdsService adsService, ICategoryService categoryService, ValidateUserSession validateUserSession)
        {
            _adService = adsService;
            _categoryService = categoryService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.Page = "adminAds";
            return View("adminAds",  await _adService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.Page = "adminAds";
            return View("SaveAd", new SaveAdViewModel()
            {
                CategoriesList = await _categoryService.GetAllViewModel()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAdViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.CategoriesList = await _categoryService.GetAllViewModel();
                return View("SaveAd", vm);
            }

            SaveAdViewModel adVm = await _adService.Add(vm);

            //if (adVm != null && adVm.Id != 0)
            //{
            //    adVm.ImagePathOne = UploadFile(vm.File, adVm.Id);
            //    await _adService.Update(adVm);
            //}

            return RedirectToRoute(new { controller = "adminAds", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.Page = "adminAds";
            SaveAdViewModel saveAdViewModel = await _adService.GetSaveViewModelById(id);
            saveAdViewModel.CategoriesList = await _categoryService.GetAllViewModel();
            return View("SaveAd", saveAdViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveAdViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

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
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            ViewBag.Page = "adminAds";
            return View("DeleteAd", await _adService.GetSaveViewModelById(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _adService.Delete(id);
            return RedirectToRoute(new { controller = "adminAds", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id)
        {
            //get directory path
            string basePath = $"/Images/Ads/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file path
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = fileInfo.Name + fileInfo.Extension;

            string fileNameWhitPath = Path.Combine(path, fileName);

            using(var stream = new FileStream(fileNameWhitPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return Path.Combine(basePath,fileName);
        }
    }
}
