using AskAbout.Data;
using AskAbout.Models;
using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace AskAbout.Services
{
    public class ManageServices : IManageServices
    {
        private readonly ApplicationDbContext _db;

        public ManageServices(
            ApplicationDbContext db)
        {
            _db = db;
        }

        public Task AddPhoto(IFormFile formFile, string path, User user)
        {           
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = DateTime.Now.Ticks + ".jpg";

            using (var stream = File.Create(path + filePath))
            {
                formFile.CopyTo(stream);
            }

            _db.Users.First(u => u.Id == user.Id).Photo = filePath;
            return _db.SaveChangesAsync();
        }
    }
}
