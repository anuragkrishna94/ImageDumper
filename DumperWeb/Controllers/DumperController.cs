using DumperApplicationCore.BusinessLogic;
using DumperWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace DumperWeb.Controllers
{
    public class DumperController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly DumpAndFetch _manager;
        public DumperController(IConfiguration configuration, DumpAndFetch manager) : base(manager)
        {
            _configuration = configuration;
            _manager = manager;
        }

        [Route("Dumper/Within/{dumperName}")]
        public IActionResult InsideDumper(string dumperName)
        {
            if(!_manager.CheckIfDumperNameIsValid(dumperName))
                return RedirectToActionPermanent("Error", "Home");
            
            List<string> imageURLs = _manager.GetImagesOfDumper(dumperName);
            for(int i = 0;i < imageURLs.Count;i++)
            {
                imageURLs[i] = Path.Combine(_configuration.GetValue<string>("ImageDumpLoc"), imageURLs.ElementAt(i));
            }
            return View(model: new DumperViewModel { DumperName = dumperName, ImageURLs = imageURLs});
        }

        public IActionResult GetImageBytes(string imagePathURL)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePathURL);
            return new FileContentResult(imageBytes, "image/png");
        }

        public IActionResult InsideDumperBin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImagesAsync(List<IFormFile> imageFiles, string dumperName)
        {
            long size = imageFiles.Sum(f => f.Length);

            await _manager.ProcessObjectUploadAsync(imageFiles, _configuration.GetValue<string>("ImageDumpLoc"), dumperName);

            return Ok(new { count = imageFiles.Count, size });
        }
    }
}
