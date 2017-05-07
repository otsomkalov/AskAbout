using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AskAbout.Controllers
{
    public class ReplyController : Controller
    {
        // GET: Reply
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reply/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reply/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reply/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reply/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reply/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reply/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reply/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}