using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AskAbout.Data;
using AskAbout.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using AskAbout.ViewModels.Replies;
using AskAbout.ViewModels.Comments;
using Microsoft.AspNetCore.Authorization;

namespace AskAbout.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly UserManager<User> _userManager;

        public CommentsController(ApplicationDbContext context,
            IHostingEnvironment appEnvironment,
            UserManager<User> userManager)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int qid, int rid, [Bind("Id,Rating,Text,Attachment")] Comment comment, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                comment.Date = DateTime.Now;
                comment.Reply = await _context.Replies.SingleOrDefaultAsync(r => r.Id == rid);
                comment.User = await _userManager.GetUserAsync(HttpContext.User);
                if (file != null)
                {
                    string fileName = DateTime.Now.Ticks.ToString() + ".jpg";
                    string dirPath = Path.Combine(_appEnvironment.WebRootPath, "Uploads");
                    Directory.CreateDirectory(dirPath);
                    string path = Path.Combine(dirPath, fileName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        await file.CopyToAsync(stream);
                        comment.Attachment = fileName;
                    }
                }
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Questions", new { id = qid });
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Reply)
                .Include(c => c.User)
                .Include(c => c.Reply.Question)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            if (comment.User != await _userManager.GetUserAsync(HttpContext.User))
            {
                return NotFound();
            }

            EditCommentViewModel model = new EditCommentViewModel()
            {
                Comment = comment,
                qid = comment.Reply.Question.Id
            };

            return View(model);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int? qid, [Bind("Id,Date,Rating,Text,Attachment")] Comment comment, IFormFile file)
        {
            if (id != comment.Id || qid == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    comment.Date = DateTime.Now;

                    if (file != null)
                    {
                        string fileName = DateTime.Now.Ticks.ToString() + ".jpg";
                        string dirPath = Path.Combine(_appEnvironment.WebRootPath, "Uploads");
                        Directory.CreateDirectory(dirPath);
                        string path = Path.Combine(dirPath, fileName);
                        using (var stream = System.IO.File.Create(path))
                        {
                            await file.CopyToAsync(stream);
                            comment.Attachment = fileName;
                        }
                    }

                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Questions", new { id = qid });
            }
            return RedirectToAction("Details", "Questions", new { id = qid });
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await _userManager.GetUserAsync(HttpContext.User);
            Reply reply = await _context.Replies.Include(r => r.Question).SingleOrDefaultAsync(r => r.Id == id);

            Comment comment = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Reply)
                .SingleOrDefaultAsync(c => c.User == user && c.Reply == reply);

            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Questions", new { id = reply.Question.Id });
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
