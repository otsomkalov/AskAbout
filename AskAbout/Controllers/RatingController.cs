using System.Linq;
using System.Threading.Tasks;
using AskAbout.Data;
using AskAbout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AskAbout.Controllers
{
    public class RatingController : Controller
    {
        private readonly AppDbContext _context;

        public RatingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Rating
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Rating.Include(r => r.Topic).Include(r => r.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Rating/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rating = await _context.Rating
                .Include(r => r.Topic)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null) return NotFound();

            return View(rating);
        }

        // GET: Rating/Create
        public IActionResult Create()
        {
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return View();
        }

        // POST: Rating/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,UserId,TopicId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", rating.TopicId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", rating.UserId);
            return View(rating);
        }

        // GET: Rating/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var rating = await _context.Rating.FindAsync(id);
            if (rating == null) return NotFound();
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", rating.TopicId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", rating.UserId);
            return View(rating);
        }

        // POST: Rating/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,UserId,TopicId")] Rating rating)
        {
            if (id != rating.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", rating.TopicId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", rating.UserId);
            return View(rating);
        }

        // GET: Rating/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var rating = await _context.Rating
                .Include(r => r.Topic)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null) return NotFound();

            return View(rating);
        }

        // POST: Rating/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rating = await _context.Rating.FindAsync(id);
            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(int id)
        {
            return _context.Rating.Any(e => e.Id == id);
        }
    }
}