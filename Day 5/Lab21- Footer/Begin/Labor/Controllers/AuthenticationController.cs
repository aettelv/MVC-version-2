using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Labor.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Labor.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoLogin(UserDetails u)
        {
            if (ModelState.IsValid)
            {
                var bal = new EmployeeBusinessLayer();
                if (bal.IsValidUser(u))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, u.UserName)
                    };

                    // create identity
                    var identity = new ClaimsIdentity(claims, "cookie");

                    // create principal
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        "AuthScheme",
                        principal);

                    return RedirectToAction("Index", "Employee");
                }

                ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                return View("Login");
            }

            return View("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                "AuthScheme");
            return RedirectToAction("Login");
        }
    }
}