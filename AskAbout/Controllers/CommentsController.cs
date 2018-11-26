using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AskAbout.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

//        // GET: Comments/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//
//            var comment = await _context.Comments
//                .Include(c => c.Reply)
//                .Include(c => c.User)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (comment == null)
//            {
//                return NotFound();
//            }
//
//            return View(comment);
//        }
//
//        // GET: Comments/Create
//        public IActionResult Create()
//        {
//            ViewData["ReplyId"] = new SelectList(_context.Replies, "Id", "Id");
//            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
//            return View();
//        }
//
//        // POST: Comments/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Text,Date,Attachment,UserId,ReplyId,Id")]
//            Comment comment)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(comment);
//                await _context.SaveChangesAsync();
//            }
//
//            ViewData["ReplyId"] = new SelectList(_context.Replies, "Id", "Id", comment.ReplyId);
//            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", comment.UserId);
//            return View(comment);
//        }
//
//        // GET: Comments/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//
//            var comment = await _context.Comments.FindAsync(id);
//            if (comment == null)
//            {
//                return NotFound();
//            }
//
//            ViewData["ReplyId"] = new SelectList(_context.Replies, "Id", "Id", comment.ReplyId);
//            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", comment.UserId);
//            return View(comment);
//        }
//
//        // POST: Comments/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Text,Date,Attachment,UserId,ReplyId,Id")]
//            Comment comment)
//        {
//            if (id != comment.Id)
//            {
//                return NotFound();
//            }
//
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(comment);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!CommentExists(comment.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//
//                return RedirectToAction(nameof(Index));
//            }
//
//            ViewData["ReplyId"] = new SelectList(_context.Replies, "Id", "Id", comment.ReplyId);
//            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", comment.UserId);
//            return View(comment);
//        }
//
//        // GET: Comments/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//
//            var comment = await _context.Comments
//                .Include(c => c.Reply)
//                .Include(c => c.User)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (comment == null)
//            {
//                return NotFound();
//            }
//
//            return View(comment);
//        }
//
//        // POST: Comments/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var comment = await _context.Comments.FindAsync(id);
//            _context.Comments.Remove(comment);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//
//        private bool CommentExists(int id)
//        {
//            return _context.Comments.Any(e => e.Id == id);
//        }
    }
}