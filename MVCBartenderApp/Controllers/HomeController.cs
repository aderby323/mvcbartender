using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCBartenderApp.Context;
using MVCBartenderApp.Models;
using MVCBartenderApp.Models.ViewModels;
using MVCBartenderApp.Services.Interfaces;
using System.Diagnostics;
using System.Security.Claims;

namespace MVCBartenderApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IBartenderService _bartenderService;

        public HomeController(IAuthService authService, IBartenderService bartenderService)
        {
            _authService = authService;
            _bartenderService = bartenderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Menu()
        {
            return View(_bartenderService.GetMenu());
        }

        [Authorize("Bartender")]
        [HttpGet]
        public IActionResult OrderQueue()
        {
            return View(_bartenderService.GetOrderQueue());
        }

        [HttpPost]
        public IActionResult PlaceOrder(int cocktailID) 
        {
            if (cocktailID <= 0) { return RedirectToAction("Menu"); }

            bool result = _bartenderService.AddOrder(cocktailID);

            if (!result) { RedirectToAction("Menu"); }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            ViewData["ErrorMessage"] = null;
            User user = _authService.ValidateLogin(login);
            if (user is null)
            {
                ViewData["ErrorMessage"] = "Username or password is incorrect.";
                return View();
            }

            ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            if (!User.Identity.IsAuthenticated) { return BadRequest("Not logged in to the system."); }

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
