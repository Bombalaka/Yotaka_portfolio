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
            new { Id = 1, Title = "Inventory Management System", Description = "A C# and in-memory data storage.", Technologies = "C#, SQLite, Entity Framework", GitHubUrl = "https://github.com/Bombalaka/Inventory_system.git", ImageUrl = "/images/inventory.jpg" },
            new { Id = 2, Title = "Expenses App", Description = "An Expenses App with in-memory data storage.", Technologies = "ASP.NET Core, Entity Framwork", GitHubUrl = "https://github.com/Bombalaka/ExpensesApp.git", ImageUrl = "https://plus.unsplash.com/premium_photo-1679784204551-013181bb687f?w=900&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8ZXhwZW5zZXMlMjBhcHB8ZW58MHx8MHx8fDA%3D" }
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