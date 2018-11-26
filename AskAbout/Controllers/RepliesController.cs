using System.Threading.Tasks;
using AskAbout.Models;
using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AskAbout.Controllers
{
    public class RepliesController : Controller
    {
        private readonly IReplyService _replyService;

        public RepliesController(IReplyService replyService)
        {
            _replyService = replyService;
        }

        // POST: Replies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reply reply, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                await _replyService.CreateAsync(reply, User, file);
                return Ok();
            }

            return BadRequest(reply);
        }

        // GET: Replies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var reply = await _replyService.GetAsync(id.Value);

            if (reply == null) return NotFound();

            return View(reply);
        }

        // POST: Replies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reply reply, IFormFile file)
        {
            if (id != reply.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _replyService.EditAsync(reply, file);
                return Ok();
            }

            return View(reply);
        }

        // POST: Replies/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await _replyService.ExistsAsync(id)) return NotFound();
            await _replyService.DeleteAsync(id);
            return Ok();
        }
    }
}