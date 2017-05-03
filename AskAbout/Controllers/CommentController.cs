using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AskAbout.Models;
using AskAbout.Data;
using AskAbout.Models.QuestionViewModels;
using AskAbout.Services;
using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace AskAbout.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IQuestionServices _questionServices;
        private readonly UserManager<User> _userManager;

        public CommentController(
            ApplicationDbContext context,
            IQuestionServices questionServices,
            UserManager<User> userManager)
        {
            _db = context;
            _questionServices = questionServices;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult WriteComment(int id)
        {
            ViewData["Replies"] = id;
            ViewData["Question"] = _db.Questions.First(q => q.Id == _db.Replies.First(r => r.Id == id).Question.Id).Id;
            return View("~/Views/Question/WriteComment.cshtml", _db.Comments.Where(q => q.Reply.Id == id).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> WriteComment(CommentViewModel model)
        {
            
            Comment MyComment = new Comment()
            {
                Date = DateTime.Now,
                User = _userManager.GetUserAsync(HttpContext.User).Result,
                Question = _db.Questions.FirstOrDefault(q => q.Id == model.Qid),
                Reply = _db.Replies.FirstOrDefault(q => q.Id == model.Rid),
                Text = model.Comment
            };

            _db.Comments.Add(MyComment);
            await _db.SaveChangesAsync();
            return RedirectToAction("WriteComment");
        }
    }
}