using DumperApplicationCore.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace DumperWeb.Controllers
{
    public class DumperController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly DumpAndFetch _manager;
        public DumperController(IConfiguration configuration, DumpAndFetch manager)
        {
            _configuration = configuration;
            _manager = manager;
        }

        [Route("Dumper/Within/{dumperName}")]
        public IActionResult InsideDumper(string dumperName)
        {
            if(_manager.CheckIfDumperNameIsValid(dumperName))
                return View(model: dumperName);
            return RedirectToActionPermanent("Error", "Home");
        }

        public IActionResult InsideDumperBin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImagesAsync(List<IFormFile> imageFiles, string dumperName)
        {
            long size = imageFiles.Sum(f => f.Length);

            await _manager.ProcessObjectUploadAsync(imageFiles, _configuration.GetValue<string>("ImageDumpLoc"), dumperName);

            return Ok(new { count = imageFiles.Count, size });
        }
    }
}
