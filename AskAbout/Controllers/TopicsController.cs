using System.Threading.Tasks;
using AskAbout.Data;
using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AskAbout.Controllers
{
    public class TopicsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IQuestionServices _questionServices;
        private readonly ITopicServices _topicServices;

        public TopicsController(ApplicationDbContext context, IQuestionServices questionServices, ITopicServices topicServices)
        {
            _context = context;
            _questionServices = questionServices;
            _topicServices = topicServices;
        }

        // GET: Topics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Topics.ToListAsync());
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(string id)
        {
            return View("../Questions/Index", await _questionServices.Get(await _topicServices.Get(id)));
        }
    }
}