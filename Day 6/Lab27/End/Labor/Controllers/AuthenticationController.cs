using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Labor.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

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
                EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
                if (bal.IsValidUser(u))
                {
                    List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, u.UserName)
                };

                    ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        scheme: "AuthScheme",
                        principal: principal);

                    //This line was added
                    HttpContext.Session.SetString("SessionKeyName", u.UserName);

                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                    return View("Login");
                }
            }
            else
            {
                return View("Login");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                scheme: "AuthScheme");
            return RedirectToAction("Login");
        }
    }
}