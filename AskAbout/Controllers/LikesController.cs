using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AskAbout.Data;
using AskAbout.Models;

namespace AskAbout.Controllers
{
    public class LikesController : Controller
    {
        private readonly AppDbContext _context;

        public LikesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Likes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Likes.Include(l => l.Comment).Include(l => l.Question).Include(l => l.Reply).Include(l => l.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Likes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Likes
                .Include(l => l.Comment)
                .Include(l => l.Question)
                .Include(l => l.Reply)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }

        // GET: Likes/Create
        public IActionResult Create()
        {
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "Id");
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id");
            ViewData["ReplyId"] = new SelectList(_context.Replies, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return View();
        }

        // POST: Likes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IsLiked,UserId,QuestionId,ReplyId,CommentId,Id")] Like like)
        {
            if (ModelState.IsValid)
            {
                _context.Add(like);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "Id", like.CommentId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", like.QuestionId);
            ViewData["ReplyId"] = new SelectList(_context.Replies, "Id", "Id", like.ReplyId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", like.UserId);
            return View(like);
        }

        // GET: Likes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Likes.FindAsync(id);
            if (like == null)
            {
                return NotFound();
            }
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "Id", like.CommentId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", like.QuestionId);
            ViewData["ReplyId"] = new SelectList(_context.Replies, "Id", "Id", like.ReplyId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", like.UserId);
            return View(like);
        }

        // POST: Likes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IsLiked,UserId,QuestionId,ReplyId,CommentId,Id")] Like like)
        {
            if (id != like.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(like);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LikeExists(like.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommentId"] = new SelectList(_context.Comments, "Id", "Id", like.CommentId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", like.QuestionId);
            ViewData["ReplyId"] = new SelectList(_context.Replies, "Id", "Id", like.ReplyId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", like.UserId);
            return View(like);
        }

        // GET: Likes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Likes
                .Include(l => l.Comment)
                .Include(l => l.Question)
                .Include(l => l.Reply)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }

        // POST: Likes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var like = await _context.Likes.FindAsync(id);
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LikeExists(int id)
        {
            return _context.Likes.Any(e => e.Id == id);
        }
    }
}
