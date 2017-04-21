using AskAbout.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.Services.Interfaces
{
    public interface IManageServices
    {
        Task AddPhoto(IFormFile formFile, string path, User user);
    }
}
