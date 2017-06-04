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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AskAbout.ViewModels.Questions;
using AskAbout.ViewModels;
using AskAbout.ViewModels.Replies;

namespace AskAbout.Controllers
{
    public class RepliesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly UserManager<User> _userManager;

        public RepliesController(ApplicationDbContext context,
            IHostingEnvironment appEnvironment,
            UserManager<User> userManager)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
        }

        // POST: Replies/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Text")] Reply reply, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                reply.Date = DateTime.Now;
                reply.User = await _userManager.GetUserAsync(HttpContext.User);
                reply.Question = await _context.Questions.SingleOrDefaultAsync(q => q.Id == id);
                if (file != null)
                {
                    string fileName = DateTime.Now.Ticks.ToString() + ".jpg";
                    string dirPath = Path.Combine(_appEnvironment.WebRootPath, "Uploads");
                    Directory.CreateDirectory(dirPath);
                    string path = Path.Combine(dirPath, fileName);

                    using (var stream = System.IO.File.Create(path))
                    {
                        await file.CopyToAsync(stream);
                        reply.Attachment = fileName;
                    }
                }
                _context.Add(reply);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Questions", new { id = id });
            }
            return RedirectToAction("Details", "Questions", new { id = id });
        }

        // GET: Replies/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies
                .Include(r => r.Question)
                .Include(r => r.User)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (reply == null)
            {
                return NotFound();
            }

            if (reply.User != await _userManager.GetUserAsync(HttpContext.User))
            {
                return NotFound();
            }

            EditReplyViewModel model = new EditReplyViewModel()
            {
                Reply = reply,
                qid = reply.Question.Id
            };

            return View(model);
        }

        // POST: Replies/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int? qid, [Bind("Id,Text,Attachment")] Reply reply, IFormFile file)
        {
            if (id != reply.Id || qid == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    reply.Date = DateTime.Now;

                    if (file != null)
                    {
                        string fileName = DateTime.Now.Ticks.ToString() + ".jpg";
                        string dirPath = Path.Combine(_appEnvironment.WebRootPath, "Uploads");
                        Directory.CreateDirectory(dirPath);
                        string path = Path.Combine(dirPath, fileName);
                        using (var stream = System.IO.File.Create(path))
                        {
                            await file.CopyToAsync(stream);
                            reply.Attachment = fileName;
                        }
                    }

                    _context.Update(reply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplyExists(reply.Id))
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
            return View(reply);
        }

        // GET: Replies/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id, int? qid)
        {
            if (id == null || qid == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies
                .Include(r => r.User)
                .Include(r => r.Question)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (reply.User != await _userManager.GetUserAsync(HttpContext.User))
            {
                return NotFound();
            }

            if (reply.Attachment != null)
            {
                System.IO.File.Delete(Path.Combine(_appEnvironment.WebRootPath, "Uploads", reply.Attachment));
            }

            _context.Replies.Remove(reply);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Questions", new { id = qid });
        }

        //// GET: Replies/Like/5
        //[HttpGet]
        //[Authorize]
        //public async Task<StatusCodeResult> Like(int id)
        //{
        //    User user = await _userManager.GetUserAsync(HttpContext.User);

        //    Reply reply = await _context.Replies
        //        .Include(r => r.User)
        //        .Include(r => r.Question)
        //            .ThenInclude(q => q.Topic)
        //        .SingleOrDefaultAsync(r => r.Id == id);

        //    Like like = await _context.Likes
        //        .SingleOrDefaultAsync(l => l.User.Equals(user) && l.Reply.Equals(reply));

        //    Rating rating = await _context.Rating
        //        .SingleOrDefaultAsync(r => r.User.Equals(reply.User) && r.Topic.Equals(reply.Question.Topic));

        //    if (like != null)
        //    {
        //        if (like.IsLiked == null)
        //        {
        //            like.IsLiked = true;
        //            rating.Amount++;
        //        }
        //        else
        //        {
        //            if (like.IsLiked == false)
        //            {
        //                like.IsLiked = true;
        //                rating.Amount += 2;
        //            }
        //            else
        //            {
        //                return StatusCode(404);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        like = new Like()
        //        {
        //            IsLiked = true,
        //            User = user,
        //            Question = question
        //        };

        //        _context.Likes.Add(like);
        //        rating.Amount++;
        //    }

        //    await _context.SaveChangesAsync();
        //    return StatusCode(200);
        //}

        //// GET: Replies/Dislike/5
        //[HttpGet]
        //[Authorize]
        //public async Task<StatusCodeResult> Dislike(int id)
        //{
        //    User user = await _userManager.GetUserAsync(HttpContext.User);

        //    Question question = await _context.Questions
        //        .Include(q => q.Topic)
        //        .Include(q => q.User)
        //        .SingleOrDefaultAsync(q => q.Id == id);

        //    Like like = await _context.Likes
        //        .SingleOrDefaultAsync(l => l.User == user && l.Question == question);

        //    Rating rating = await _context.Rating
        //        .SingleOrDefaultAsync(r => r.User == question.User && r.Topic == question.Topic);

        //    if (like != null)
        //    {
        //        if (like.IsLiked == null)
        //        {
        //            like.IsLiked = false;
        //            rating.Amount--;
        //        }
        //        else
        //        {
        //            if (like.IsLiked == true)
        //            {
        //                like.IsLiked = false;
        //                rating.Amount -= 2;
        //            }
        //            else
        //            {
        //                return StatusCode(404);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        like = new Like()
        //        {
        //            IsLiked = false,
        //            User = user,
        //            Question = question
        //        };

        //        _context.Likes.Add(like);
        //        rating.Amount--;
        //    }

        //    await _context.SaveChangesAsync();
        //    return StatusCode(200);
        //}

        //// GET: Replies/ResetLike/5
        //[HttpGet]
        //[Authorize]
        //public async Task<StatusCodeResult> ResetLike(int? id)
        //{
        //    var user = await _userManager.GetUserAsync(HttpContext.User);

        //    if (user == null)
        //    {
        //        return StatusCode(404);
        //    }

        //    Question question = await _context.Questions
        //        .Include(q => q.Topic)
        //        .SingleOrDefaultAsync(q => q.Id == id);

        //    Like like = await _context.Likes
        //        .Include(l => l.Question)
        //            .ThenInclude(q => q.User)
        //        .Include(l => l.User)
        //        .SingleOrDefaultAsync(l => l.Question == question && l.User == user);

        //    Rating rating = await _context.Rating
        //        .SingleOrDefaultAsync(r => r.Topic == question.Topic && r.User == question.User);

        //    if (like == null)
        //    {
        //        return StatusCode(404);
        //    }

        //    if (like.IsLiked == true)
        //    {
        //        rating.Amount--;
        //    }
        //    else
        //    {
        //        rating.Amount++;
        //    }

        //    like.IsLiked = null;

        //    await _context.SaveChangesAsync();
        //    return StatusCode(200);
        //}

        private bool LikeExists(int id)
        {
            return _context.Likes.Any(e => e.Id == id);
        }

        private bool ReplyExists(int id)
        {
            return _context.Replies.Any(e => e.Id == id);
        }
    }
}
