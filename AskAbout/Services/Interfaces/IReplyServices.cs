using System.Threading.Tasks;
using AskAbout.Models;
using Microsoft.AspNetCore.Http;

namespace AskAbout.Services.Interfaces
{
    public interface IReplyServices
    {
        Task<Reply> Get(int id);

        Task<int> Create(Reply reply, User user, IFormFile file);

        Task<int> Edit(Reply reply, IFormFile file);

        Task<int> Delete(int id);
    }
}