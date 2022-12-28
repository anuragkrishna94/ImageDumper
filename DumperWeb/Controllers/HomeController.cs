using DumperApplicationCore.BusinessLogic;
using DumperWeb.Helper;
using DumperWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DumperWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DumpAndFetch _manager;

        public HomeController(ILogger<HomeController> logger, DumpAndFetch manager)
        {
            _logger = logger;
            _manager = manager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateDumper()
        {
            string dumperName = _manager.CreateDumper();
            return RedirectToAction($"InsideDumper", "Dumper", new { dumperName });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}