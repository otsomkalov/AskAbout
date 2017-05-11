using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskAbout.Models;
using AskAbout.Data;
using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace AskAbout.Services
{
    public class QuestionServices : IQuestionServices
    {
        private readonly ApplicationDbContext _db;

        public QuestionServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Question> Get(int id)
        {
            return await _db.Questions
                .Include(q => q.User)
                .Include(q => q.Topic)
                .SingleAsync(q => q.Id == id);
        }

        public async Task<List<Question>> Get()
        {
            return await _db.Questions
                .Include(q => q.Likes)
                .Include(q => q.User)
                .Include(q => q.Topic)
                .ToListAsync();
        }

        public async Task<List<Question>> Get(Topic topic)
        {
            return await _db.Questions
                .Include(q => q.Likes)
                .Include(q => q.User)
                .Include(q => q.Topic)
                .Where(q => q.Topic == topic)
                .ToListAsync();
        }

        public async Task<List<Question>> GetRecent()
        {
            return await _db.Questions
                .Include(q => q.Likes)
                .Include(q => q.User)
                .Include(q => q.Topic)
                .OrderBy(q => q.Date)
                .ToListAsync();
        }

        public async Task<List<Question>> GetPopular()
        {
            return await _db.Questions
                .Include(q => q.Likes)
                .Include(q => q.User)
                .Include(q => q.Topic)
                .OrderBy(q => q.LikesCount)
                .ToListAsync();
        }

        public Task Add(string text, string topic, User user)
        {
            Question question = new Question()
            {
                Date = DateTime.Now,
                Text = text,
                TopicTitle = topic,
                UserId = user.Id,
            };

            _db.Topics.First(t => t.Title == question.TopicTitle).QuestionsCount++;
            _db.Questions.Add(question);

            return _db.SaveChangesAsync();
        }

        public Task AddAttachment(IFormFile file, string path, Question question)
        {
            throw new NotImplementedException();
        }

        public Task Edit(string text, Question question, int id)
        {
            question.Text = text;
            return _db.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            Question question = _db.Questions.First(q => q.Id == id);
            Topic topic = _db.Topics.First(t => question.TopicTitle == t.Title);
            topic.RepliesCount -= question.RepliesCount;
            topic.QuestionsCount--;
            topic.Rating -= question.LikesCount;
            _db.Questions.Remove(question);

            return _db.SaveChangesAsync();
        }

        public Task Like(int questionId, User user)
        {
            Question question = _db.Questions.First(q => q.Id == questionId);
            question.LikesCount++;
            _db.Topics.First(t => t.Title == question.TopicTitle).Rating++;

            Like like = new Like
            {
                UserId = user.Id,
                QuestionId = questionId
            };

            _db.Likes.Add(like);
            question.Likes.Add(like);
            user.Likes.Add(like);

            return _db.SaveChangesAsync();
        }

        public Task Dislike(int questionId, User user)
        {
            Question question = _db.Questions.First(q => q.Id == questionId);
            question.LikesCount--;
            _db.Topics.First(t => t.Title == question.TopicTitle).Rating--;

            Like like = new Like
            {
                UserId = user.Id,
                QuestionId = questionId
            };

            _db.Likes.Remove(like);

            return _db.SaveChangesAsync();
        }


    }
}
