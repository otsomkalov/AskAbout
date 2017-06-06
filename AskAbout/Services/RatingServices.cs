using System.Threading.Tasks;
using AskAbout.Data;
using AskAbout.Models;
using AskAbout.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AskAbout.Services
{
    public class RatingServices : IRatingServices
    {
        private readonly ApplicationDbContext _context;

        public RatingServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(User user, Topic topic)
        {
            _context.Rating.Add(new Rating()
            {
                Topic = topic,
                User = user
            });

            await _context.SaveChangesAsync();
        }

        public async Task<Rating> Get(User user, Topic topic)
        {
            return await _context.Rating
                .Include(r => r.User)
                .Include(r => r.Topic)
                .SingleOrDefaultAsync(r => r.User.Equals(user) && r.Topic.Equals(topic));
        }
    }
}