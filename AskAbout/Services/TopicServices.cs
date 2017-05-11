using AskAbout.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskAbout.Models;
using AskAbout.Data;
using Microsoft.EntityFrameworkCore;

namespace AskAbout.Services
{
    public class TopicServices:ITopicServices
    {
        private readonly ApplicationDbContext _db;

        public TopicServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Topic> Get(string title)
        {
            return await _db.Topics
                .SingleAsync(t => t.Title == title);
        }

        public async Task<List<Topic>> GetTopics()
        {
            return await _db.Topics.ToListAsync();
        }
    }
}
