using System.Collections.Generic;
using System.Threading.Tasks;
using AskAbout.Models;
using Microsoft.AspNetCore.Http;

namespace AskAbout.Services.Interfaces
{
    public interface IQuestionServices
    {
        Task<Question> Get(int id);

        Task<List<Question>> Get();

        Task<List<Question>> Get(string title);

        Task<List<Question>> Get(Topic topic);

        Task<List<Question>> GetRecent();

        Task<List<Question>> GetPopular();

        Task Create(Question question, User user, IFormFile file);

        Task Edit(int id, Question question, IFormFile file);

        Task Delete(int id);
    }
}