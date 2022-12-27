using DumperApplicationCore.BusinessLogic;
using DumperWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DumperWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DumpAndFetch _manager;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, DumpAndFetch manager)
        {
            _configuration = configuration;
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
            _manager.CreateDumper();
            return RedirectToActionPermanent("InsideDumper");
        }

        public IActionResult InsideDumper()
        {
            return View("InsideDumperBin");
        }

        public IActionResult InsideDumperBin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImagesAsync(List<IFormFile> imageFiles)
        {
            long size = imageFiles.Sum(f => f.Length);

            foreach(var formFile in imageFiles)
            {
                if(formFile.Length > 0)
                {
                    string fileName = Path.Combine(_configuration.GetValue<string>("ImageDumpLoc"), $"{Guid.NewGuid()}.png");

                    using var stream = System.IO.File.Create(fileName);
                    await formFile.CopyToAsync(stream);
                }
            }

            return Ok(new {statuc = imageFiles.Count, count = imageFiles.Count, size });
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