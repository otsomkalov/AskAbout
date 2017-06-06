using System.Threading.Tasks;
using AskAbout.Models;

namespace AskAbout.Services.Interfaces
{
    public interface ILikeServices
    {
        Task<Like> Get(Question question, User user);

        Task<Like> Get(Reply reply, User user);

        Task<Like> Get(Comment comment, User user);

        Task<bool> Like(Question question, User user);

        Task<bool> Like(Reply reply, User user);

        Task<bool> Like(Comment comment, User user);

        Task<bool> Dislike(Question question, User user);

        Task<bool> Dislike(Reply reply, User user);

        Task<bool> Dislike(Comment comment, User user);

        Task RemoveLike(Question question, User user);

        Task RemoveLike(Reply reply, User user);

        Task RemoveLike(Comment comment, User user);
    }
}