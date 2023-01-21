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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OgrenciKayit.Controllers
{
    public class StudentsController : Controller
    {
        // Add service injections here
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;
        private readonly ILessonService _lessonService;
        public StudentsController(IStudentService studentService, IClassService classService, ILessonService lessonService)
        {
            _studentService = studentService;
            _classService = classService;
            _lessonService = lessonService;
        }

        // GET: Students
        public IActionResult Index()
        {
            List<StudentModel> studentList = _studentService.Query().ToList(); // TODO: Add get list service logic here
            return View(studentList);
        }

        // GET: Students/Details/5
        public IActionResult Details(int id)
        {
            StudentModel student = _studentService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_classService.Query().ToList(), "Id", "Name");
            ViewBag.Lessons = new MultiSelectList(_lessonService.Query().ToList(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(StudentModel student, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                bool? imageResult = UpdateImage(student, image);
                if(imageResult == false)
                {
                    ModelState.AddModelError("", $"Image upload failed! Accepted extensions" +
                        $" {AppSettings.AcceptedImageExtensions} and maximum image size (MB){AppSettings.AcceptedImageLength}");
                    ViewData["ClassId"] = new SelectList(_classService.Query().ToList(), "Id", "Name");
                    ViewBag.Lessons = new MultiSelectList(_lessonService.Query().ToList(), "Id", "Name");
                }
                var result = _studentService.Add(student);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            ViewData["ClassId"] = new SelectList(_classService.Query().ToList(), "Id", "Name");
            ViewBag.Lessons = new MultiSelectList(_lessonService.Query().ToList(), "Id", "Name");

            return View(student);
        }


        private bool? UpdateImage(StudentModel model, IFormFile image)
        {
            #region Validation
            bool? result = null;
            string uploadedFileName = null, uploadedFileExtension = null;
            if (image is not null && image.Length > 0)
            {
                result = false; 
                uploadedFileName = image.FileName; 
                uploadedFileExtension = Path.GetExtension(uploadedFileName); 
                string[] acceptedImageExtensions = AppSettings.AcceptedImageExtensions.Split(',');
                result = acceptedImageExtensions.Any(acceptedImageExtension => acceptedImageExtension.ToLower().Trim() == uploadedFileExtension.ToLower());
                if (result == true)
                {
                    double acceptedImageLength = AppSettings.AcceptedImageLength * Math.Pow(1024, 2); // bytes
                    if (image.Length > acceptedImageLength)
                        result = false;
                }
            }
            #endregion

            #region Kayıt
            if (result == true)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    image.CopyTo(memoryStream);
                    model.Image = memoryStream.ToArray();
                    model.ImgExtension = uploadedFileExtension;
                }
            }
            #endregion
            return result;
        }


        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            StudentModel student = _studentService.Query().SingleOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_classService.Query().ToList(), "Id", "Name", student.ClassId);
            ViewBag.Lessons = new MultiSelectList(_lessonService.Query().ToList(), "Id", "Name", student.LessonIds);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StudentModel student , IFormFile image)
        {
            if (ModelState.IsValid)
            {
                bool? imageResult = UpdateImage(student, image);
                if (imageResult == false)
                {
                    ModelState.AddModelError("", $"Image upload failed! Accepted extensions {AppSettings.AcceptedImageExtensions} and maximum image size (MB) {AppSettings.AcceptedImageLength}");
                    ViewData["ClassId"] = new SelectList(_classService.Query().ToList(), "Id", "Name", student.ClassId);
                    ViewBag.Lessons = new MultiSelectList(_lessonService.Query().ToList(), "Id", "Name", student.LessonIds);
                    return View(student);
                }
                var result = _studentService.Update(student);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_classService.Query().ToList(), "Id", "Name", student.ClassId);
            ViewBag.Lessons = new MultiSelectList(_lessonService.Query().ToList(), "Id", "Name", student.LessonIds);
            return View(student);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int id)
        {
            StudentModel student = _studentService.Query().SingleOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Delete
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _studentService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteImage(int id)
        {
            _studentService.DeleteImage(id);
            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
