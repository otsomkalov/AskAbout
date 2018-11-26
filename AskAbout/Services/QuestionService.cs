using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AskAbout.Data;
using AskAbout.Models;
using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AskAbout.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;
        private readonly UserManager<User> _userManager;

        public QuestionService(AppDbContext context, IHostingEnvironment appEnvironment, IFileService fileService,
            UserManager<User> userManager)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _fileService = fileService;
            _userManager = userManager;
        }

        public Task<Question> GetByIdAsync(int id)
        {
            return _context.Questions
                .Include(q => q.Likes)
                .ThenInclude(l => l.User)
                .Include(q => q.Replies)
                .ThenInclude(r => r.Comments)
                .ThenInclude(c => c.Likes)
                .ThenInclude(l => l.User)
                .Include(q => q.Replies)
                .ThenInclude(r => r.User)
                .Include(q => q.Replies)
                .ThenInclude(r => r.Likes)
                .ThenInclude(l => l.User)
                .Include(q => q.Topic)
                .Include(q => q.User)
                .SingleOrDefaultAsync(q => q.Id == id);
        }

        public Task<Question[]> ListAsync()
        {
            return _context.Questions
                .Include(q => q.Likes)
                .Include(q => q.Topic)
                .Include(q => q.User)
                .Where(q => q.IsActive)
                .ToArrayAsync();
        }

        public Task<Question[]> GetByTopicAsync(Topic topic)
        {
            return _context.Questions
                .Include(q => q.Likes)
                .ThenInclude(l => l.User)
                .Include(q => q.Replies)
                .ThenInclude(r => r.Comments)
                .Include(q => q.Replies)
                .Include(q => q.Topic)
                .Include(q => q.User)
                .Where(q => q.Topic.Equals(topic))
                .ToArrayAsync();
        }

        public Task<Question[]> GetByTitleAsync(string title)
        {
            return _context.Questions
                .Where(q => q.Title.ToLower().Contains(title.ToLower()))
                .ToArrayAsync();
        }

        public async Task<Question[]> GetPopularAsync()
        {
            return await _context.Questions
                .Include(q => q.Likes).ThenInclude(l => l.User)
                .Include(q => q.Replies).ThenInclude(r => r.Comments)
                .Include(q => q.Replies)
                .Include(q => q.Topic)
                .Include(q => q.User)
                .OrderByDescending(q => q.Likes.Count)
                .ToArrayAsync();
        }

        public Task<Question[]> GetRecentAsync()
        {
            return _context.Questions
                .Include(q => q.Likes).ThenInclude(l => l.User)
                .Include(q => q.Replies).ThenInclude(r => r.Comments)
                .Include(q => q.Topic)
                .Include(q => q.User)
                .OrderByDescending(q => q.Date)
                .ToArrayAsync();
        }

        public async Task CreateAsync(Question question, ClaimsPrincipal user, IFormFile file)
        {
            var filePath = await _fileService.SaveFileAsync<Question>(file);
            question.Attachment = filePath;
            question.User = await _userManager.GetUserAsync(user);
            question.Date = DateTime.Now;
            _context.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Question question)
        {
            var dbQuestion = await GetByIdAsync(question.Id);

            dbQuestion.Title = question.Title;
            dbQuestion.Text = question.Text;


            _context.Update(dbQuestion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            question.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Questions.AnyAsync(question => question.Id == id);
        }

        public async Task DeleteAsync(Question question)
        {
            if (question.Attachment != null)
                File.Delete(Path.Combine(_appEnvironment.WebRootPath, "Uploads", question.Attachment));

            foreach (var reply in question.Replies)
            {
                if (reply.Attachment != null)
                    File.Delete(Path.Combine(_appEnvironment.WebRootPath, "Uploads", reply.Attachment));

                _context.Replies.Remove(reply);
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}