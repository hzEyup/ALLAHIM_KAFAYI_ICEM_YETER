using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace MvcWebUI.ViewComponents
{
    public class ClassesViewComponent : ViewComponent
    {
        private readonly IClassService _classService;

        public ClassesViewComponent(IClassService classService)
        {
            _classService = classService;
        }

        public ViewViewComponentResult Invoke()
        {
            List<ClassModel> classes;


            var task = _classService.GetListAsync();
            classes = task.Result;
            return View(classes);
        }
    }
}
