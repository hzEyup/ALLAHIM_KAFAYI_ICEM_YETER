using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcWebUI.Areas.Reports.Models;

namespace MvcWebUI.Areas.Reports.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Reports")] 
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IClassService _classService;

        public HomeController(IReportService reportService, IClassService classService)
        {
            _reportService = reportService;
            _classService = classService;
        }

        public IActionResult Index(HomeIndexViewModel viewModel)
        {
            viewModel.Report = _reportService.GetListInnerJoin(viewModel.Filter);
            viewModel.Classes = new SelectList(_classService.Query().ToList(), "Id", "Name");
            return View(viewModel);
        }
    }
}
