using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PCHUBStore.Areas.Administration.Controllers
{
    public class BlacksmithController : AdministrationController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditUserIndexPage()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult EditUserCategoryPage()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Success(string message)
        {
            this.ViewBag.message = message;
            return this.View();
        }
    }
}