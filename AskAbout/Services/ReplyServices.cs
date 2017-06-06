using System;
using System.IO;
using System.Threading.Tasks;
using AskAbout.Data;
using AskAbout.Models;
using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AskAbout.Services
{
    public class ReplyServices : IReplyServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IQuestionServices _questionServices;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IRatingServices _ratingServices;

        public ReplyServices(ApplicationDbContext context, IQuestionServices questionServices, IHostingEnvironment appEnvironment, IRatingServices ratingServices)
        {
            _context = context;
            _questionServices = questionServices;
            _appEnvironment = appEnvironment;
            _ratingServices = ratingServices;
        }

        public async Task<Reply> Get(int id)
        {
            return await _context.Replies
                .Include(r => r.User)
                .Include(r => r.Question).ThenInclude(q=>q.Topic)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int> Create(Reply reply, User user, IFormFile file)
        {
            reply.Date=DateTime.Now;
            reply.User = user;
            reply.Question = await _questionServices.Get(reply.Question.Id);

            var rating = await _context.Rating
                .SingleOrDefaultAsync(r => r.User.Equals(reply.User) && r.Topic.Equals(reply.Question.Topic));

            if (rating == null)
            {
                await _ratingServices.Create(user, reply.Question.Topic);
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
                    reply.Attachment = fileName;
                }
            }

            _context.Replies.Add(reply);
            await _context.SaveChangesAsync();
            return reply.Question.Id;
        }

        public async Task<int> Delete(int id)
        {
            var reply = await Get(id);

            if (reply.Attachment != null)
                File.Delete(Path.Combine(_appEnvironment.WebRootPath, "Uploads", reply.Attachment));

            _context.Replies.Remove(reply);
            await _context.SaveChangesAsync();
            return reply.Question.Id;
        }

        public async Task<int> Edit(Reply reply, IFormFile file)
        {
            var dbReply = await Get(reply.Id);
            
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