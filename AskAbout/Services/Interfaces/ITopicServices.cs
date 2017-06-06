using System.Collections.Generic;
using System.Threading.Tasks;
using AskAbout.Models;

namespace AskAbout.Services.Interfaces
{
    public interface ITopicServices
    {
        Task<List<Topic>> Get();

        Task<Topic> Get(string title);
    }
}