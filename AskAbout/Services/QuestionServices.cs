using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AskAbout.Data;
using AskAbout.Models;
using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AskAbout.Services
{
    public class QuestionServices : IQuestionServices
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly ITopicServices _topicServices;
        private readonly IRatingServices _ratingServices;

        public QuestionServices(ApplicationDbContext context, ITopicServices topicServices,
            IHostingEnvironment appEnvironment, IRatingServices ratingServices)
        {
            _context = context;
            _topicServices = topicServices;
            _appEnvironment = appEnvironment;
            _ratingServices = ratingServices;
        }

        public async Task<Question> Get(int id)
        {
            return await _context.Questions
                .Include(q => q.Likes).ThenInclude(l => l.User)
                .Include(q => q.Replies).ThenInclude(r => r.Comments).ThenInclude(c => c.Likes).ThenInclude(l => l.User)
                .Include(q => q.Replies).ThenInclude(r => r.User)
                .Include(q => q.Replies).ThenInclude(r => r.Likes).ThenInclude(l=>l.User)
                .Include(q => q.Topic)
                .Include(q => q.User)
                .SingleOrDefaultAsync(q => q.Id == id);
        }

        public async Task<List<Question>> Get()
        {
            return await _context.Questions
                .Include(q => q.Likes).ThenInclude(l => l.User)
                .Include(q => q.Replies).ThenInclude(r => r.Comments)
                .Include(q => q.Replies)
                .Include(q => q.Topic)
                .Include(q => q.User)
                .ToListAsync();
        }

        public async Task<List<Question>> Get(Topic topic)
        {
            return await _context.Questions
                .Include(q => q.Likes).ThenInclude(l => l.User)
                .Include(q => q.Replies).ThenInclude(r => r.Comments)
                .Include(q => q.Replies)
                .Include(q => q.Topic)
                .Include(q => q.User)
                .Where(q => q.Topic.Equals(topic))
                .ToListAsync();
        }

        public async Task<List<Question>> Get(string title)
        {
            return await _context.Questions
                .Include(q => q.Likes).ThenInclude(l => l.User)
                .Include(q => q.Replies).ThenInclude(r => r.Comments)
                .Include(q => q.Replies)
                .Include(q => q.Topic)
                .Include(q => q.User)
                .Where(q => q.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();
        }

        public async Task<List<Question>> GetPopular()
        {
            return await _context.Questions
                .Include(q => q.Likes).ThenInclude(l => l.User)
                .Include(q => q.Replies).ThenInclude(r => r.Comments)
                .Include(q => q.Replies)
                .Include(q => q.Topic)
                .Include(q => q.User)
                .OrderByDescending(q => q.Likes.Count)
                .ToListAsync();
        }

        public async Task<List<Question>> GetRecent()
        {
            return await _context.Questions
                .Include(q => q.Likes).ThenInclude(l => l.User)
                .Include(q => q.Replies).ThenInclude(r => r.Comments)
                .Include(q => q.Replies)
                .Include(q => q.Topic)
                .Include(q => q.User)
                .OrderByDescending(q => q.Date)
                .ToListAsync();
        }

        public async Task Create(Question question, User user, IFormFile file)
        {
            question.User = user;
            question.Date = DateTime.Now;
            question.Topic = await _topicServices.Get(question.Topic.Name);

            var rating = await _context.Rating
                .SingleOrDefaultAsync(r => r.User.Equals(question.User) && r.Topic.Equals(question.Topic));

            if (rating == null)
            {
                await _ratingServices.Create(question.User, question.Topic);
            }

            if (file != null)
            {
                var fileName = DateTime.Now.Ticks + ".jpg";
                var dirPath = Path.Combine(_appEnvironment.WebRootPath, "Uploads");
                Directory.CreateDirectory(dirPath);
                var path = Path.Combine(dirPath, fileName);
                using (Stream stream = File.Create(path))
                {
                    await file.CopyToAsync(stream);
                    question.Attachment = fileName;
                }
            }

            _context.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var question = await Get(id);

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

        public async Task Edit(int id, Question question, IFormFile file)
        {
            var dbQuestion = await Get(question.Id);

            dbQuestion.Title = question.Title;
            dbQuestion.Text = question.Text;
            dbQuestion.Date = DateTime.Now;

            if (file != null)
            {
                var fileName = DateTime.Now.Ticks + ".jpg";
                var dirPath = Path.Combine(_appEnvironment.WebRootPath, "Uploads");
                Directory.CreateDirectory(dirPath);
                var path = Path.Combine(dirPath, fileName);
                using (Stream stream = File.Create(path))
                {
                    await file.CopyToAsync(stream);
                    dbQuestion.Attachment = fileName;
                }
            }

            _context.Update(dbQuestion);
            await _context.SaveChangesAsync();
        }
    }
}