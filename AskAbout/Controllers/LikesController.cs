using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AskAbout.Data;
using AskAbout.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace AskAbout.Controllers
{
    [Authorize]
    public class LikesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly UserManager<User> _userManager;

        public LikesController(ApplicationDbContext context,
            IHostingEnvironment appEnvironment,
            UserManager<User> userManager)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
        }

        // GET: Likes/Like
        public async Task<StatusCodeResult> Like(int id)
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            Question question = await _context.Questions.SingleOrDefaultAsync(q => q.Id == id);
            Like like = await _context.Likes.SingleOrDefaultAsync(l => l.User == user && l.Question == question);

            if (like != null)
            {
                if (like.IsLiked == false)
                {
                    like.IsLiked = true;
                    _context.Update(like);
                }
                else
                {
                    return StatusCode(404);
                }
            }
            else
            {
                like = new Like()
                {
                    IsLiked = true,
                    User = user,
                    Question = question
                };
                _context.Add(like);
            }
            
            await _context.SaveChangesAsync();
            return StatusCode(200);
        }

        [HttpGet]
        public async Task<StatusCodeResult> Dislike(int id)
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            Question question = await _context.Questions.SingleOrDefaultAsync(q => q.Id == id);
            Like like = await _context.Likes.SingleOrDefaultAsync(l => l.User == user && l.Question == question);

            if (like != null)
            {
                if (like.IsLiked == true)
                {
                    like.IsLiked = false;
                    _context.Update(like);
                }
                else
                {
                    return StatusCode(404);
                }
            }
            else
            {
                like = new Like()
                {
                    IsLiked = false,
                    User = user,
                    Question = question
                };
                _context.Add(like);
            }
            
            await _context.SaveChangesAsync();
            return StatusCode(200);
        }

        // GET: Likes/Delete/5
        public async Task<StatusCodeResult> Delete(int? id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return StatusCode(404);
            }

            var like = await _context.Likes
                .Include(l => l.Question)
                .Include(l => l.User)
                .SingleOrDefaultAsync(l => l.Question.Id == id && l.User == user);

            if (like == null)
            {
                return StatusCode(404);
            }

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
            return StatusCode(200);
        }

        private bool LikeExists(int id)
        {
            return _context.Likes.Any(e => e.Id == id);
        }
    }
}
