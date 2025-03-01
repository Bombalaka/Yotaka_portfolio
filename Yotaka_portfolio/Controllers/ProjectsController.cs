using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Yotaka_portfolio.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(ILogger<ProjectsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
    {
        
        var projects = new[]
        {
            new { Id = 1, Title = "Inventory Management System", Description = "A C# and SQLite inventory system.", Technologies = "C#, SQLite, Entity Framework", GitHubUrl = "https://github.com/Yotaka/InventoryApp", ImageUrl = "/images/inventory.jpg" },
            new { Id = 2, Title = "Hostel Booking Web App", Description = "An MVC-based hostel booking system.", Technologies = "ASP.NET Core, MongoDB", GitHubUrl = "https://github.com/Yotaka/HostelBooking", ImageUrl = "/images/hostel.jpg" }
        };

        var project = projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }

        ViewData["ProjectTitle"] = project.Title;
        ViewData["Description"] = project.Description;
        ViewData["Technologies"] = project.Technologies;
        ViewData["GitHubUrl"] = project.GitHubUrl;
        ViewData["ImageUrl"] = project.ImageUrl;

        return View("Details");
    }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        
    }
}