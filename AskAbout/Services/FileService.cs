using System;
using System.IO;
using System.Threading.Tasks;
using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AskAbout.Services
{
    public class FileService : IFileService
    {
        public async Task<string> SaveFileAsync<T>(IFormFile file)
        {
            var targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", nameof(T));
            Directory.CreateDirectory(targetDirectory);

            var fileInfo = new FileInfo(file.FileName);
            var fileName = $"{Guid.NewGuid().ToString()}{fileInfo.Extension}";

            using (var stream = File.Create(Path.Combine(targetDirectory, fileName)))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}