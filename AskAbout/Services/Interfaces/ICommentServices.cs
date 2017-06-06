using System.Threading.Tasks;
using AskAbout.Models;
using Microsoft.AspNetCore.Http;

namespace AskAbout.Services.Interfaces
{
    public interface ICommentServices
    {
        Task<Comment> Get(int id);

        Task<int> Create(Comment comment, User user, IFormFile file);

        Task<int> Edit(Comment comment, IFormFile file);

        Task<int> Delete(int id);
    }
}