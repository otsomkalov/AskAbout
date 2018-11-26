using System.Threading.Tasks;
using AskAbout.Models;

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
        Task CreateAsync(Question question);
        Task UpdateAsync(Question question);
        Task DeleteAsync(int id);
    }
}