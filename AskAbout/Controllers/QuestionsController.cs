using System.Threading.Tasks;
using AskAbout.Data;
using AskAbout.Models;
using AskAbout.Services.Interfaces;
using AskAbout.ViewModels.Questions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AskAbout.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IQuestionServices _questionServices;
        private readonly ITopicServices _topicServices;
        private readonly UserManager<User> _userManager;
        private readonly ILikeServices _likeServices;

        public QuestionsController(ApplicationDbContext context,
            IHostingEnvironment appEnvironment,
            UserManager<User> userManager,
            IQuestionServices questionServices, ITopicServices topicServices, ILikeServices likeServices)
        {
            _userManager = userManager;
            _questionServices = questionServices;
            _topicServices = topicServices;
            _likeServices = likeServices;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            return View(await _questionServices.Get());
        }

        // GET: Questions/Recent
        public async Task<IActionResult> Recent()
        {
            return View("Index", await _questionServices.GetRecent());
        }

        // GET: Questions/Popular
        public async Task<IActionResult> Popular()
        {
            return View("Index", await _questionServices.GetPopular());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _questionServices.Get(id));
        }

        // GET: Questions/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = new CreateQuestionViewModel
            {
                Topics = await _topicServices.Get()
            };

            return View(model);
        }

        // POST: Questions/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Question question,
            IFormFile file)
        {
            await _questionServices.Create(question, await _userManager.GetUserAsync(HttpContext.User), file);
            return RedirectToActionPermanent("Index");
        }

        // GET: Questions/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _questionServices.Get(id));
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Question question,
            IFormFile file)
        {
            await _questionServices.Edit(id, question, file);
            return RedirectToAction("Index");
        }

        // GET: Questions/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _questionServices.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<JsonResult> Search(string title)
        {
            if (title != null)
            {
                var results = await _questionServices.Get(title);

                return Json(new { results });
            }
            return Json(new { });
        }

        // GET: Questions/Like/5
        [HttpGet]
        [Authorize]
        public async Task<StatusCodeResult> Like(int id)
        {
            if (await _likeServices.Like(await _questionServices.Get(id), await _userManager.GetUserAsync(HttpContext.User))) return StatusCode(200);

            return StatusCode(404);
        }

        // GET: Questions/Dislike/5
        [HttpGet]
        [Authorize]
        public async Task<StatusCodeResult> Dislike(int id)
        {
            if (await _likeServices.Dislike(await _questionServices.Get(id), await _userManager.GetUserAsync(HttpContext.User))) return StatusCode(200);
            return StatusCode(404);
        }

        // GET: Questions/ResetLike/5
        [HttpGet]
        [Authorize]
        public async Task<StatusCodeResult> ResetLike(int id)
        {
            await _likeServices.RemoveLike(await _questionServices.Get(id), await _userManager.GetUserAsync(HttpContext.User));
            return StatusCode(200);
        }
    }
}