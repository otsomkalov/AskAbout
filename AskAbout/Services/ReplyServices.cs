using AskAbout.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AskAbout.Data;
using System.IO;
using AskAbout.Models;

namespace AskAbout.Services
{
    public class ReplyServices : IReplyServices
    {
        private readonly ApplicationDbContext _db;

        public ReplyServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task Add(string text, int id, User user)
        {
            Question question = _db.Questions.First(q => q.Id == id);
            question.RepliesCount++;
            _db.Topics.First(t => t.Title == question.TopicTitle).RepliesCount++;

            Reply Reply = new Reply()
            {
                Date = DateTime.Now,
                User = user,
                Question = question,
                Text = text
            };

            _db.Replies.Add(Reply);
            return _db.SaveChangesAsync();
        }

        public Task AddAttachment(IFormFile file, string path, Reply reply)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = DateTime.Now.Ticks + ".jpg";

            using (var stream = File.Create(path + filePath))
            {
                file.CopyTo(stream);
            }

            _db.Replies.Find(reply).Attachment = filePath;
            return _db.SaveChangesAsync();
        }

        public List<Reply> GetReplies(int id)
        {
            var replies = _db
                .Replies
                .Where(reply => reply.Question.Id == id)
                .ToList();

            foreach (Reply Reply in replies)
            {
                Reply.User = _db.Users.First(u => u.Id == Reply.UserId);
            }

            return replies;
        }

        
    }
}
