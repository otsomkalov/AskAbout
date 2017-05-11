using System;
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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AskAbout.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IQuestionServices _questionServices;
        private readonly UserManager<User> _userManager;
        private readonly ITopicServices _topicServices;
        private readonly IReplyServices _replyServices;

        public QuestionController(
            ApplicationDbContext context,
            IQuestionServices questionServices,
            UserManager<User> userManager,
            ITopicServices topicServices,
            IReplyServices replyServices)
        {
            _db = context;
            _questionServices = questionServices;
            _userManager = userManager;
            _topicServices = topicServices;
            _replyServices = replyServices;
        }

        [HttpPost]
        public async Task Like(int id)
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _questionServices.Like(id, user);
        }

        [HttpPost]
        public async Task Dislike(int id)
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _questionServices.Dislike(id, user);
        }

        [HttpGet]
        public async Task<IActionResult> Questions()
        {
            return View(await _questionServices.Get());
        }

        [HttpGet]
        public async Task<IActionResult> Question(int id)
        {
            var replies = _replyServices.GetReplies(id);
            if (replies != null)
            {
                ViewData["Replies"] = replies;
                return View(await _questionServices.Get(id));
            }
            else
            {
                return RedirectToAction("Questions");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Recent()
        {
            return View("Questions", await _questionServices.GetRecent());
        }

        [HttpGet]
        public async Task<IActionResult> Popular()
        {
            return View("Questions", await _questionServices.GetPopular());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add()
        {
            return View(await _topicServices.GetTopics());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddQuestionViewModel model)
        {
            await _questionServices.Add(model.Text, model.Topic, await _userManager.GetUserAsync(HttpContext.User));
            return RedirectToAction("Questions");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var question = await _questionServices.Get(id);

            if (question.User == await _userManager.GetUserAsync(HttpContext.User))
            {
                return View(question);
            }
            else
            {
                return RedirectToAction("Questions");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Edit(EditQuestionViewModel model)
        {
            var question = await _questionServices.Get(model.Qid);

            if (question.User == await _userManager.GetUserAsync(HttpContext.User))
            {
                await _questionServices.Edit(model.Text, question, model.Qid);
                return RedirectToAction("Questions");
            }
            else
            {
                return RedirectToAction("Questions");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await _questionServices.Get(id)).User.Equals(await _userManager.GetUserAsync(HttpContext.User)))
            {
                await _questionServices.Delete(id);
                return RedirectToAction("Questions");
            }
            else
            {
                return RedirectToAction("Questions");
            }
        }

        [HttpGet]
        public async Task<IActionResult> SelectedQuestions(string id)
        {
            ViewBag.User = await _userManager.GetUserAsync(HttpContext.User);

            return View("Questions", await _questionServices.Get(await _topicServices.Get(id)));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Reply(ReplyViewModel model)
        {
            await _replyServices.Add(model.Reply, model.Qid, _userManager.GetUserAsync(HttpContext.User).Result);
            return RedirectToAction("Question", new { id = model.Qid });
        }

        [HttpGet]
        public async Task<IActionResult> Topics()
        {
            return View(await _topicServices.GetTopics());
        }
    }
}
