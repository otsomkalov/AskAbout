using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskAbout.Models;
using AskAbout.Data;
using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AskAbout.Services
{
    public class QuestionServices : IQuestionServices
    {
        private readonly ApplicationDbContext _db;

        public QuestionServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task Add(string text, string topic, User user)
        {
            var Question = new Question()
            {
                Date = DateTime.Now,
                Text = text,
                TopicTitle = topic,
                UserId = user.Id,
            };
            _db.Questions.Add(Question);

            return _db.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            _db.Questions.Remove(_db.Questions.Find(id));
            return _db.SaveChangesAsync();
        }

        public Task Edit(string text, Question question, int id)
        {
            question.Text = text;
            return _db.SaveChangesAsync();
        }

        public List<Reply> GetReplies(int id)
        {
            var question = _db.Questions.First(q => q.Id == id);
            if (question != null)
            {
                var replies = _db
               .Replies
               .Where(reply => reply.Question == question)
               .ToList();

                foreach (Reply Reply in replies)
                {
                    Reply.User = _db.Users.First(u => u.Id == Reply.UserId);
                }

                return replies;
            }
            else
            {
                return null;
            }
        }

        public Task Reply(string text, int id, User user)
        {
            Reply Reply = new Reply()
            {
                Date = DateTime.Now,
                User = user,
                Question = _db.Questions.First(q => q.Id == id),
                Text = text
            };

            _db.Replies.Add(Reply);
            return _db.SaveChangesAsync();
        }

        public Task Like(int id)
        {
            _db.Questions.First(q => q.Id == id).Likes++;
            return _db.SaveChangesAsync();
        }

        public Task Dislike()
        {
            return _db.SaveChangesAsync();
        }
    }
}
