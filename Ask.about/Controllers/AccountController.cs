using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ask.about.Models;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ask.about.Controllers
{
    public class AccountController : Controller
    {
        private UserContext db;

        public AccountController(UserContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> Login(User user)
        {
            User res = db.Users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
            if (res != null)
            {
                await Authenticate(res.Email);

                return RedirectToRoutePermanent(new
                {
                    controller = "Questions",
                    action = "Questions",
                });
            }
            else
            {
                return RedirectToRoutePermanent(new
                {
                    controller = "Account",
                    action = "Login",
                });
            }
        }

        private async Task Authenticate(string Login)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,Login)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.Authentication.SignInAsync("Cookie", new ClaimsPrincipal(id));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Login");
        }
    }
}
