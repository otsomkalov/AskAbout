using System.Threading.Tasks;
using AskAbout.Data;
using AskAbout.Models;
using AskAbout.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AskAbout.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public Task Update(User user)
        {
            var u = _context.Entry(user);
            u.State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}