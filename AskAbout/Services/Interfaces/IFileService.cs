using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AskAbout.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync<T>(IFormFile file);
    }
}