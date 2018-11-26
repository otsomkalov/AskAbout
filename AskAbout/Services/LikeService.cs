using System.Linq;
using System.Threading.Tasks;
using AskAbout.Data;
using AskAbout.Models;
using AskAbout.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AskAbout.Services
{
    public class LikeService : ILikeService
    {
        private readonly AppDbContext _context;
        private readonly IRatingService _ratingService;

        public LikeService(AppDbContext context, IRatingService ratingService, IReplyService replyService)
        {
            _context = context;
            _ratingService = ratingService;
        }

        public async Task<Like> Get(Question question, User user)
        {
            return await _context.Likes
                .Include(l => l.Question)
                .Include(l => l.User)
                .SingleOrDefaultAsync(l => l.User.Equals(user) && l.Question != null && l.Question.Equals(question));
        }

        public async Task<Like> Get(Reply reply, User user)
        {
            return await _context.Likes
                .Include(l => l.User)
                .Include(l => l.Reply)
                .SingleOrDefaultAsync(l => l.User.Equals(user) && l.Reply != null && l.Reply.Equals(reply));
        }

        public async Task<Like> Get(Comment comment, User user)
        {
            return await _context.Likes
                .Include(l => l.Comment)
                .Include(l => l.User)
                .SingleOrDefaultAsync(l => l.User.Equals(user) && l.Comment != null && l.Comment.Equals(comment));
        }

        public async Task<bool> Like(Question question, User user)
        {
            var like = await Get(question, user);
            var rating = await _ratingService.Get(question.User, question.Topic);

            if (like != null)
            {
                switch (like.IsLiked)
                {
                    case null:
                        like.IsLiked = true;
                        rating.Amount++;
                        break;
                    case false:
                        like.IsLiked = true;
                        rating.Amount += 2;
                        break;
                    default:
                        return false;
                }
            }
            else
            {
                like = new Like
                {
                    IsLiked = true,
                    User = user,
                    Question = question
                };

                _context.Likes.Add(like);
                rating.Amount++;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Like(Reply reply, User user)
        {
            var like = await Get(reply, user);
            var rating = await _ratingService.Get(reply.User, reply.Question.Topic);

            if (like != null)
            {
                switch (like.IsLiked)
                {
                    case null:
                        like.IsLiked = true;
                        rating.Amount++;
                        break;
                    case false:
                        like.IsLiked = true;
                        rating.Amount += 2;
                        break;
                    default:
                        return false;
                }
            }
            else
            {
                like = new Like
                {
                    IsLiked = true,
                    User = user,
                    Reply = reply
                };

                _context.Likes.Add(like);
                rating.Amount++;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Like(Comment comment, User user)
        {
            var like = await Get(comment, user);
            var rating = await _ratingService.Get(comment.User, comment.Reply.Question.Topic);

            if (like != null)
            {
                switch (like.IsLiked)
                {
                    case null:
                        like.IsLiked = true;
                        rating.Amount++;
                        break;
                    case false:
                        like.IsLiked = true;
                        rating.Amount += 2;
                        break;
                    default:
                        return false;
                }
            }
            else
            {
                like = new Like
                {
                    IsLiked = true,
                    User = user,
                    Comment = comment
                };

                _context.Likes.Add(like);
                rating.Amount++;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Dislike(Question question, User user)
        {
            var like = await Get(question, user);
            var rating = await _ratingService.Get(question.User, question.Topic);

            if (like != null)
            {
                switch (like.IsLiked)
                {
                    case null:
                        like.IsLiked = false;
                        rating.Amount--;
                        break;
                    case true:
                        like.IsLiked = false;
                        rating.Amount -= 2;
                        break;
                    default:
                        return false;
                }
            }
            else
            {
                like = new Like
                {
                    IsLiked = false,
                    User = user,
                    Question = question
                };

                _context.Likes.Add(like);
                rating.Amount--;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Dislike(Reply reply, User user)
        {
            var like = await Get(reply, user);
            var rating = await _ratingService.Get(reply.User, reply.Question.Topic);

            if (like != null)
            {
                switch (like.IsLiked)
                {
                    case null:
                        like.IsLiked = false;
                        rating.Amount--;
                        break;
                    case true:
                        like.IsLiked = false;
                        rating.Amount -= 2;
                        break;
                    default:
                        return false;
                }
            }
            else
            {
                like = new Like
                {
                    IsLiked = false,
                    User = user,
                    Reply = reply
                };

                _context.Likes.Add(like);
                rating.Amount--;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Dislike(Comment comment, User user)
        {
            var like = await Get(comment, user);
            var rating = await _ratingService.Get(comment.User, comment.Reply.Question.Topic);

            if (like != null)
            {
                switch (like.IsLiked)
                {
                    case null:
                        like.IsLiked = false;
                        rating.Amount--;
                        break;
                    case true:
                        like.IsLiked = false;
                        rating.Amount -= 2;
                        break;
                    default:
                        return false;
                }
            }
            else
            {
                like = new Like
                {
                    IsLiked = false,
                    User = user,
                    Comment = comment
                };

                _context.Likes.Add(like);
                rating.Amount--;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task RemoveLike(Question question, User user)
        {
            var like = await Get(question, user);
            var rating = await _ratingService.Get(question.User, question.Topic);

            if (like.IsLiked == true)
                rating.Amount--;
            else
                rating.Amount++;

            like.IsLiked = null;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveLike(Reply reply, User user)
        {
            var like = await Get(reply, user);
            var rating = await _ratingService.Get(reply.User, reply.Question.Topic);

            if (like.IsLiked == true)
                rating.Amount--;
            else
                rating.Amount++;

            like.IsLiked = null;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveLike(Comment comment, User user)
        {
            var like = await Get(comment, user);
            var rating = await _ratingService.Get(comment.User, comment.Reply.Question.Topic);

            if (like.IsLiked == true)
                rating.Amount--;
            else
                rating.Amount++;

            like.IsLiked = null;

            await _context.SaveChangesAsync();
        }
    }
}