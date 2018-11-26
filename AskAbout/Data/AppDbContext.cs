using AskAbout.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AskAbout.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Rating> Rating { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Topic>().HasData(DbInitializer.GetInitialData());
            base.OnModelCreating(builder);
        }
    }
}