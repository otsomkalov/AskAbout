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

        [Authorize]
        public IActionResult Questions()
        {
            return View(db.Questions.ToList());
        }

        [Authorize,HttpGet]
        public IActionResult Add()
        {
            return View(db.Topics.ToList());
        }

        [Authorize,HttpPost]
        public async Task<IActionResult> Add(Question question)
        {
            question.Date = DateTime.Now;
            question.Topic = db.Topics.Find();
            db.Questions.Add(question);
            await db.SaveChangesAsync();
            return RedirectToAction("Questions");
        }
    }
}
