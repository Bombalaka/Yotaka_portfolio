using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Yotaka_portfolio.Controllers
{
    
    public class DigitalArtController : Controller
    {
        private readonly ILogger<DigitalArtController> _logger;

        public DigitalArtController(ILogger<DigitalArtController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
    {
        // Sample data 
        var artworks = new[]
        {
            new { Id = 1, Title = "Sunset Over Nan", Description = "A digital painting of a sunset in Nan, Thailand.", ImageUrl = "/images/IMG_0208.jpeg", DownloadUrl = "#" },
            new { Id = 2, Title = "Cyberpunk Samurai", Description = "A futuristic samurai concept art.", ImageUrl = "/images/IMG_0225.jpeg", DownloadUrl = "#" }
        };

        var artwork = artworks.FirstOrDefault(a => a.Id == id);
        if (artwork == null)
        {
            return NotFound();
        }

        ViewData["ArtworkTitle"] = artwork.Title;
        ViewData["Description"] = artwork.Description;
        ViewData["ImageUrl"] = artwork.ImageUrl;
        ViewData["DownloadUrl"] = artwork.DownloadUrl;

        return View("Details");
    }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}