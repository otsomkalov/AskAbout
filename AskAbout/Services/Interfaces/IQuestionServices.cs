using AskAbout.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.Services.Interfaces
{
    public interface IQuestionServices
    {
        Question Get(int id);

        List<Question> Get();

        List<Question> Get(Topic topic);

        List<Question> GetRecent();

        List<Question> GetPopular();

        Task Add(string text, string topic, User user);

        Task AddAttachment(IFormFile file, string path, Question question);

        Task Edit(string text, Question question, int id);

        Task Delete(int id);             

        Task Like(int questionId, User user);

        Task Dislike(int questionId, User user);
    }
}
