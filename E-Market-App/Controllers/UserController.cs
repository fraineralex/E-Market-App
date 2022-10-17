using EMarketApp.Core.Application.Interfaces.Services;
using EMarketApp.Core.Application.Services;
using EMarketApp.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Pokedex_App.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersService _userService;

        public UserController(IUsersService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            ViewBag.Page = "login";
            return View("login");
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.Page = "login";
                return View("Login", loginViewModel);

            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult Register()
        {
            ViewBag.Page = "Sing up";
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel saveUserViewModel)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.Page = "Sing up";
                return View("Register", saveUserViewModel);

            }

            await _userService.Add(saveUserViewModel);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

    }
}
