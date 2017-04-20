using AskAbout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskAbout.Services.Interfaces
{
    public interface IQuestionServices
    {
        Task Add(string text, string topic, User user);

        Task Delete(int id);

        Task Edit(string text, Question question, int id);

        List<Reply> GetReplies(int id);

        Task Reply(string text, int id, User user);

        Task Like(int id);

        Task Dislike();
    }
}
