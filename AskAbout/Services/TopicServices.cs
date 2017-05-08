using AskAbout.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskAbout.Models;
using AskAbout.Data;

namespace AskAbout.Services
{
    public class TopicServices:ITopicServices
    {
        private readonly ApplicationDbContext _db;

        public TopicServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public Topic Get(string title)
        {
            return _db.Topics
                .First(t => t.Title == title);
        }

        public List<Topic> GetTopics()
        {
            return _db.Topics.ToList();
        }
    }
}
