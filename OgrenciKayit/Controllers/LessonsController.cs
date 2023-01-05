#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business.Services;
using Business.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OgrenciKayit.Controllers
{
    public class LessonsController : Controller
    {
        // Add service injections here
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        // GET: Lessons
        public IActionResult Index()
        {
            List<LessonModel> lessonList = _lessonService.Query().ToList();
            return View(lessonList);
        }

        // GET: Lessons/Details/5
        public IActionResult Details(int id)
        {
            LessonModel lesson = _lessonService.Query().SingleOrDefault(l => l.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LessonModel lesson)
        {
            if (ModelState.IsValid)
            {
                var result = _lessonService.Add(lesson);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public IActionResult Edit(int id)
        {
            LessonModel lesson = _lessonService.Query().SingleOrDefault(l => l.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(lesson);
        }

        // POST: Lessons/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LessonModel lesson)
        {
            if (ModelState.IsValid)
            {
                var result = _lessonService.Update(lesson);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public IActionResult Delete(int id)
        {
            LessonModel lesson = _lessonService.Query().SingleOrDefault(l => l.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _lessonService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
