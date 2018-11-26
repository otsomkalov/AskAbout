using System;
using System.Threading.Tasks;
using AskAbout.Models;
using AskAbout.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AskAbout.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ITopicService _topicService;

        public QuestionsController(IQuestionService questionService, ITopicService topicService)
        {
            _questionService = questionService;
            _topicService = topicService;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var questions = await _questionService.ListAsync();
            return View(questions);
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var question = await _questionService.GetByIdAsync(id.Value);

            if (question == null || !question.IsActive) return NotFound();

            return View(question);
        }

        // GET: Questions/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewData["TopicId"] = new SelectList(await _topicService.ListAsync(), "Id", "Name");
            return View();
        }

        // POST: Questions/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Question question, IFormFile file)
        {
            if (ModelState.IsValid && file != null)
            {
                await _questionService.CreateAsync(question, User, file);
                return RedirectToAction(nameof(Index));
            }

            ViewData["TopicId"] = new SelectList(await _topicService.ListAsync(), "Id", "Name", question.TopicId);
            return View(question);
        }


        // GET: Questions/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var question = await _questionService.GetByIdAsync(id.Value);

            if (question == null) return NotFound();

            ViewData["TopicId"] = new SelectList(await _topicService.ListAsync(), "Id", "Name", question.TopicId);
            return View(question);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Question question)
        {
            if (id != question.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _questionService.UpdateAsync(question);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["TopicId"] = new SelectList(await _topicService.ListAsync(), "Id", "Name", question.TopicId);
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await _questionService.ExistsAsync(id)) return NotFound();

            await _questionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}