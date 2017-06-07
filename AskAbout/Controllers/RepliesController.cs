using System.Threading.Tasks;
using AskAbout.Data;
using AskAbout.Models;
using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AskAbout.Controllers
{
    [Authorize]
    public class RepliesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IReplyServices _replyServices;
        private readonly ILikeServices _likeServices;
        private readonly IQuestionServices _questionServices;

        public RepliesController(ApplicationDbContext context,
            IHostingEnvironment appEnvironment,
            UserManager<User> userManager, IReplyServices replyServices, ILikeServices likeServices, IQuestionServices questionServices)
        {
            _userManager = userManager;
            _replyServices = replyServices;
            _likeServices = likeServices;
            _questionServices = questionServices;
        }

        //Partial
        //GET:Replies/Create/id
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            return PartialView(new Reply()
            {
                Question = await _questionServices.Get(id)
            });
        }

        // POST: Replies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reply reply, IFormFile file)
        {
            await _replyServices.Create(reply, await _userManager.GetUserAsync(HttpContext.User), file);
            return RedirectToAction("Details", "Questions", new { reply.Question.Id });
        }

        // GET: Replies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _replyServices.Get(id));
        }

        // POST: Replies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reply reply,
            IFormFile file)
        {
            int qid = await _replyServices.Edit(reply, file);
            return RedirectToAction("Details", "Questions", new { id = qid });
        }

        // GET: Replies/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            int qid = await _replyServices.Delete(id);
            return RedirectToAction("Details", "Questions", new { id = qid });
        }

        // GET: Replies/Like/5
        [HttpGet]
        [Authorize]
        public async Task<StatusCodeResult> Like(int id)
        {
            if (await _likeServices.Like(await _replyServices.Get(id), await _userManager.GetUserAsync(HttpContext.User))) return StatusCode(200);

            return StatusCode(404);
        }

        // GET: Replies/Dislike/5
        [HttpGet]
        [Authorize]
        public async Task<StatusCodeResult> Dislike(int id)
        {
            if (await _likeServices.Dislike(await _replyServices.Get(id), await _userManager.GetUserAsync(HttpContext.User))) return StatusCode(200);
            return StatusCode(404);
        }

        // GET: Replies/ResetLike/5
        [HttpGet]
        [Authorize]
        public async Task<StatusCodeResult> ResetLike(int id)
        {
            await _likeServices.RemoveLike(await _replyServices.Get(id), await _userManager.GetUserAsync(HttpContext.User));
            return StatusCode(200);
        }
    }
}