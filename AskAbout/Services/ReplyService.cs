using System;
using System.IO;
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
    public class ReplyService : IReplyService
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;
        private readonly IQuestionService _questionService;
        private readonly IRatingService _ratingService;
        private readonly UserManager<User> _userManager;

        public ReplyService(AppDbContext context, IQuestionService questionService, IHostingEnvironment appEnvironment,
            IRatingService ratingService, IFileService fileService, UserManager<User> userManager)
        {
            _context = context;
            _questionService = questionService;
            _appEnvironment = appEnvironment;
            _ratingService = ratingService;
            _fileService = fileService;
            _userManager = userManager;
        }

        public async Task<Reply> GetAsync(int id)
        {
            return await _context.Replies
                .Include(r => r.User)
                .Include(r => r.Question).ThenInclude(q => q.Topic)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int> CreateAsync(Reply reply, ClaimsPrincipal user, IFormFile file)
        {
            var attachment = await _fileService.SaveFileAsync<Reply>(file);
            reply.Attachment = attachment;
            reply.Date = DateTime.Now;
            reply.User = await _userManager.GetUserAsync(user);
            reply.QuestionId = reply.Question.Id;

            _context.Replies.Add(reply);
            await _context.SaveChangesAsync();
            return reply.Question.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var reply = await _context.Replies.FindAsync(id);
            reply.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Replies.AnyAsync(reply => reply.Id == id);
        }

        public async Task<int> EditAsync(Reply reply, IFormFile file)
        {
            var dbReply = await GetAsync(reply.Id);

            dbReply.Text = reply.Text;
            dbReply.Date = DateTime.Now;

            if (file != null)
            {
                var fileName = DateTime.Now.Ticks + ".jpg";
                var dirPath = Path.Combine(_appEnvironment.WebRootPath, "Uploads");
                Directory.CreateDirectory(dirPath);
                var path = Path.Combine(dirPath, fileName);
                using (Stream stream = File.Create(path))
                {
                    await file.CopyToAsync(stream);
                    dbReply.Attachment = fileName;
                }
            }

            _context.Update(dbReply);
            await _context.SaveChangesAsync();
            return dbReply.Question.Id;
        }
    }
}