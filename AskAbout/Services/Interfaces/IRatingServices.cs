using System.Threading.Tasks;
using AskAbout.Models;

namespace AskAbout.Services.Interfaces
{
    public interface IRatingServices
    {
        Task<Rating> Get(User user, Topic topic);

        Task Create(User user, Topic topic);
    }
}