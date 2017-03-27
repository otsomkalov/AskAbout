using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ask.about.Models;

namespace Ask.about.Controllers
{
    public class LoginController : Controller
    {
        private UserContext db;

        public LoginController(UserContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Login(User user)
        {
            User res = db.Users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
            if (res != null)
            {
                //return RedirectToAction("Login");
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
                    controller = "Login",
                    action = "Login",
                });
            }
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
