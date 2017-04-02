using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ask.about.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ask.about.Controllers
{
    public class QuestionsController : Controller
    {
        private UserContext db;

        public QuestionsController(UserContext context)
        {
            db = context;
        }

        [Authorize, HttpGet]
        public IActionResult Questions()
        {
            return View(db.Questions.ToList());
        }

        [Authorize, HttpGet]
        public IActionResult Add()
        {
            return View(db.Topics.ToList());
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> Add(Question question)
        {
            question.Date = DateTime.Now;
            question.User = db.Users.First(u => u.Id.ToString() == User.Identity.Name);
            db.Questions.Add(question);
            await db.SaveChangesAsync();
            return RedirectToAction("Questions");
        }

        [Authorize, HttpGet]
        public IActionResult Edit(int id)
        {
            return View(db.Questions.First(q => q.Id == id));
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            db.Questions.Remove(db.Questions.Find(id));
            await db.SaveChangesAsync();
            return RedirectToAction("Questions");
        }
    }
}
