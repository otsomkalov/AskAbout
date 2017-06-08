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

        public async Task<Rating> Create(User user, Topic topic)
        {
            var rating = new Rating()
            {
                Topic = topic,
                User = user
            };
            _context.Rating.Add(rating);

            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating> Get(User user, Topic topic)
        {
            return await _context.Rating
                       .Include(r => r.User)
                       .Include(r => r.Topic)
                       .SingleOrDefaultAsync(r => r.User.Equals(user) && r.Topic.Equals(topic)) ??
                   await Create(user, topic);
        }
    }
}