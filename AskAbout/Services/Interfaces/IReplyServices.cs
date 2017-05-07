using AskAbout.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.Services.Interfaces
{
    public interface IReplyServices
    {
        Task AddAttachment(IFormFile file, string path, Reply reply);

        List<Reply> GetReplies(int id);

        Task Add(string text, int id, User user);
    }
}
