using System.Security.Claims;
using System.Threading.Tasks;
using AskAbout.Models;
using Microsoft.AspNetCore.Http;

namespace AskAbout.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<Question> GetByIdAsync(int id);
        Task<Question[]> ListAsync();
        Task<Question[]> GetByTitleAsync(string title);
        Task<Question[]> GetByTopicAsync(Topic topic);
        Task<Question[]> GetRecentAsync();
        Task<Question[]> GetPopularAsync();
        Task CreateAsync(Question question, ClaimsPrincipal user, IFormFile file);
        Task UpdateAsync(Question question);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}