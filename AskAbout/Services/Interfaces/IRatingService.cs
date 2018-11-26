using System.Threading.Tasks;
using AskAbout.Models;

namespace AskAbout.Services.Interfaces
{
    public interface IRatingService
    {
        Task<Rating> Get(User user, Topic topic);

        Task<Rating> Create(User user, Topic topic);
    }
}