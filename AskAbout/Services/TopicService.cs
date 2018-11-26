using System.Threading.Tasks;
using AskAbout.Data;
using AskAbout.Models;
using AskAbout.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AskAbout.Services
{
    public class TopicService : ITopicService
    {
        private readonly AppDbContext _context;

        public TopicService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Topic[]> ListAsync()
        {
            return await _context.Topics
                .Include(t => t.Questions)
                .Include(t => t.Rating)
                .ToArrayAsync();
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