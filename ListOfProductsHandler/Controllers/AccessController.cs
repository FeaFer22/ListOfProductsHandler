using ListOfProductsHandler.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ListOfProductsHandler.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Products");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "user.json");
            var jsonDeserialized = JsonConvert.DeserializeObject<List<User>>(System.IO.File.ReadAllText(filePath));

            if (user.userName.ToLower() == jsonDeserialized[0].userName
                && user.userPassword.ToLower() == jsonDeserialized[0].userPassword)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.userName),
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

            ViewData["ValidateMessage"] = "Пользователь не найден";
            return View();
        }
    }
}
