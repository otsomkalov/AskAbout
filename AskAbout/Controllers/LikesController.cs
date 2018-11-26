using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AskAbout.Controllers
{
    public class LikesController : Controller
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }
    }
}