using AskAbout.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AskAbout.Data;
using System.IO;
using AskAbout.Models;

namespace AskAbout.Services
{
    public class CommentServices : ICommentServices
    {
        private readonly ApplicationDbContext _db;

        public CommentServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task AddAttachment(IFormFile file, string path, Comment comment)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = DateTime.Now.Ticks + ".jpg";

            using (var stream = File.Create(path + filePath))
            {
                file.CopyTo(stream);
            }

            _db.Comments.SingleOrDefault(c=>c.Id==comment.Id).Attachment = filePath;
            return _db.SaveChangesAsync();
        }
    }
}
