using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.Models;
using PCHUBStore.Services.EmailSender;

namespace PCHUBStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender sender;

        public HomeController(ILogger<HomeController> logger, IEmailSender sender)
        {
            _logger = logger;
            this.sender = sender;
        }

        public async Task<IActionResult> Index(string[] some)
        {
         //   await sender.SendEmailAsync("velislav1@students.softuni.bg", "test", "test");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }


}
