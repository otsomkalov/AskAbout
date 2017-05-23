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

            Question question = await _context.Questions
                .Include(q => q.Topic)
                .Include(q => q.User)
                .SingleOrDefaultAsync(q => q.Id == id);

            Like like = await _context.Likes
                .SingleOrDefaultAsync(l => l.User == user && l.Question == question);

            Rating rating = await _context.Rating.SingleOrDefaultAsync(r => r.User == question.User && r.Topic == question.Topic);

            if (like != null)
            {
                if (like.IsLiked == null)
                {
                    like.IsLiked = true;
                    rating.Amount++;
                }
                else
                {
                    if (like.IsLiked == false)
                    {
                        like.IsLiked = true;
                        rating.Amount += 2;
                    }
                    else
                    {
                        return StatusCode(404);
                    }
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

                _context.Likes.Add(like);
                rating.Amount++;
            }

            await _context.SaveChangesAsync();
            return StatusCode(200);
        }

        [HttpGet]
        public async Task<StatusCodeResult> Dislike(int id)
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);

            Question question = await _context.Questions
                .Include(q => q.Topic)
                .Include(q => q.User)
                .SingleOrDefaultAsync(q => q.Id == id);

            Like like = await _context.Likes.SingleOrDefaultAsync(l => l.User == user && l.Question == question);

            Rating rating = await _context.Rating.SingleOrDefaultAsync(r => r.User == question.User && r.Topic == question.Topic);

            if (like != null)
            {
                if (like.IsLiked == null)
                {
                    like.IsLiked = false;
                    rating.Amount--;
                }
                else
                {
                    if (like.IsLiked == true)
                    {
                        like.IsLiked = false;
                        rating.Amount -= 2;
                    }
                    else
                    {
                        return StatusCode(404);
                    }
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

                _context.Likes.Add(like);
                rating.Amount--;
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

            Question question = await _context.Questions
                .Include(q => q.Topic)
                .SingleOrDefaultAsync(q => q.Id == id);

            Like like = await _context.Likes
                .Include(l => l.Question)
                    .ThenInclude(q => q.User)
                .Include(l => l.User)
                .SingleOrDefaultAsync(l => l.Question == question && l.User == user);

            Rating rating = await _context.Rating
                .SingleOrDefaultAsync(r => r.Topic == question.Topic && r.User == question.User);

            if (like == null)
            {
                return StatusCode(404);
            }

            if (like.IsLiked == true)
            {
                rating.Amount--;
            }
            else
            {
                rating.Amount++;
            }

            like.IsLiked = null;

            await _context.SaveChangesAsync();
            return StatusCode(200);
        }

        private bool LikeExists(int id)
        {
            return _context.Likes.Any(e => e.Id == id);
        }
    }
}
