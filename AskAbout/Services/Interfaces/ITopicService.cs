using System.Threading.Tasks;
using AskAbout.Models;

namespace AskAbout.Services.Interfaces
{
    public interface ITopicService
    {
        Task<Topic[]> ListAsync();
        Task<Topic> Get(string title);
    }
}