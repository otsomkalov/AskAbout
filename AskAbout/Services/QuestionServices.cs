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

        public Task Edit(string text, User user, int id)
        {
            //Question Question = _db.Questions.Find(question.Id);
            //Queryable.Text = question.Text;
            //_dbEntry.TopicTitle = question.TopicTitle;
            return _db.SaveChangesAsync();
        }

        public List<Reply> GetReplies(int id)
        {
            var replies = _db
                .Replies
                .Where(reply => reply.QuestionId == id)
                .ToList();

            foreach (var item in replies)
            {
                item.User = _db.Users.Find(item.UserId);
            }

            return replies;
        }

        public Task Reply(string text,int id,User user)
        {
            Reply Reply = new Reply()
            {
                Date = DateTime.Now,
                User = user.Id,
                QuestionId = id,
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
