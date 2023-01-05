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
    public class ClassesController : Controller
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        public IActionResult Index()
        {
            List<ClassModel> classList = _classService.Query().ToList();
            return View(classList);
        }

        public IActionResult Details(int id)
        {
            ClassModel @class = _classService.Query().SingleOrDefault(c => c.Id == id);
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

        public IActionResult Create()
        {
			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClassModel @class)
        {
            if (ModelState.IsValid)
            {
                var result = _classService.Add(@class);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(@class);
        }

        public IActionResult Edit(int id)
        {
            ClassModel @class = _classService.Query().SingleOrDefault(c => c.Id == id); 
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClassModel @class)
        {
            if (ModelState.IsValid)
            {
                var result = _classService.Update(@class);
                if(result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));

                }
                ModelState.AddModelError("", result.Message);
            }
          
            return View(@class);
        }

        // GET: Classes/Delete/5
        public IActionResult Delete(int id)
        {
            ClassModel @class = _classService.Query().SingleOrDefault(c => c.Id == id); 
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _classService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
