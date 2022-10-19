using EMarketApp.Core.Application.Interfaces.Services;
using EMarketApp.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using EMarketApp.Core.Application.Helpers;
using E_Market_App.Middlewares;

namespace Pokedex_App.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersService _userService;
        private readonly ValidateUserSession _validateUserSession;

        public UserController(IUsersService userService, ValidateUserSession validateUserSession)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
        }

        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            ViewBag.Page = "login";
            return View("login");
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {

                ViewBag.Page = "login";
                return View("Login", loginViewModel);

            }

            UserViewModel userVm = await _userService.Login(loginViewModel);

            if(userVm != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userVaidation", "Incorrect username or password");
            }

            ViewBag.Page = "login";
            return View("Login", loginViewModel);
        }

        public IActionResult Register()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            ViewBag.Page = "Sing up";
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel saveUserViewModel)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {

                ViewBag.Page = "Sing up";
                return View("Register", saveUserViewModel);

            }

            await _userService.Add(saveUserViewModel);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
