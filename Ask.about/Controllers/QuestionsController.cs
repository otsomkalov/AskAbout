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
            var entry = db.Questions.First(q => q.Id == id);
            ViewData["Text"] = entry.Text;
            return View(entry);
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            db.Questions.Remove(db.Questions.Find(id));
            await db.SaveChangesAsync();
            return RedirectToAction("Questions");
        }


        [HttpPost]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                Question dbEntry = db.Questions.Find(question.Id);
                dbEntry.Text = question.Text;
                dbEntry.TopicTitle = question.TopicTitle;
                db.SaveChanges();
                return RedirectToAction("Questions");
            }
            else
            {
                return View(question);
            }
        }


        [HttpGet]
        public ActionResult Reply(int id)
        {
            var replies = db
                .Replies
                .Where(reply => reply.QuestionId == id)
                .ToList();
            foreach (var item in replies)
            {
                item.User = db.Users.Find(item.UserId);
            }
            ViewData["Replies"] = replies;
            ViewData["id"] = id;
            return View(db.Questions.First(q => q.Id == id));
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> Reply(Reply answer, int qid)
        {
            answer.Date = DateTime.Now;
            answer.User = db.Users.First(u => u.Id.ToString() == User.Identity.Name);
            answer.QuestionId = qid;
            db.Replies.Add(answer);
            await db.SaveChangesAsync();
            return RedirectToActionPermanent("Reply", qid);
        }

    }
}
