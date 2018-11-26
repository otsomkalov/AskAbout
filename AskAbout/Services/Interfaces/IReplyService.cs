using System.Security.Claims;
using System.Threading.Tasks;
using AskAbout.Models;
using Microsoft.AspNetCore.Http;

namespace AskAbout.Services.Interfaces
{
    public interface IReplyService
    {
        Task<Reply> GetAsync(int id);
        Task<int> CreateAsync(Reply reply, ClaimsPrincipal user, IFormFile file);
        Task<int> EditAsync(Reply reply, IFormFile file);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}