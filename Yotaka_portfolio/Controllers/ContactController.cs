using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Yotaka_portfolio.Models;

namespace Yotaka_portfolio.Controllers
{

    public class ContactController : Controller
    {
        private static List<Contact> _contacts = new List<Contact>();


        [HttpGet]
        public IActionResult Contact()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            
            if (!ModelState.IsValid)
            {
               
                return View(contact);
            }

            if (_contacts.Any(c=> c.Email == contact.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(contact);
            }
                _contacts.Add(contact);
                TempData["SuccessMessage"] = $"Thank you for your message, {contact.Name}";
                return RedirectToAction(nameof(Contact));
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}