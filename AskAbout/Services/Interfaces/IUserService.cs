using System.Threading.Tasks;
using AskAbout.Models;

namespace AskAbout.Services.Interfaces
{
    public interface IUserService
    {
        Task Update(User user);
    }
}