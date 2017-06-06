using System.Collections.Generic;
using System.Threading.Tasks;
using AskAbout.Data;
using AskAbout.Models;
using AskAbout.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AskAbout.Services
{
    public class TopicServices : ITopicServices
    {
        private readonly ApplicationDbContext _context;

        public TopicServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Topic>> Get()
        {
            return await _context.Topics
                .Include(t => t.Questions)
                .Include(t => t.Rating)
                .ToListAsync();
        }

        public async Task<Topic> Get(string title)
        {
            return await _context.Topics
                .Include(t => t.Questions)
                .Include(t => t.Rating)
                .SingleOrDefaultAsync(t => t.Name == title);
        }
    }
}