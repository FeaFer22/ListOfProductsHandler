using ListOfProductsHandler.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ListOfProductsHandler.Controllers
{
    public class AccessController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AuthorizationOptions _authorization;

        public AccessController(IConfiguration configuration)
        {
            _configuration = configuration;
            _authorization = new AuthorizationOptions();
            configuration.GetSection("AuthorizationOptions").Bind(_authorization);
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Products");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(AuthorizationOptions user)
        {


            if (user.Name.ToLower() == _authorization.Name
                && user.Password.ToLower() == _authorization.Password)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("OtherProperties", "Example Role")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, 
                    CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties authenticationProperties = new AuthenticationProperties()
                {
                    AllowRefresh = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authenticationProperties);

                return RedirectToAction("Index", "Products");
            }

            ViewData["ValidateMessage"] = "Вход не выполнен";
            return View();
        }
    }
}
