using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PCHUBStore.Areas.Administration.Controllers
{
    public class FiltersControllerController : AdministrationController
    {
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateCategory(string category)
        {
            return View();
        }


        [HttpGet]
        public IActionResult CreateFilter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFilter(string category)
        {
            return View();
        }


    }
}