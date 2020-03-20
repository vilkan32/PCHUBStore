using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PCHUBStore.Areas.Administration.Controllers
{
    public class MainSliderController : AdministrationController
    {
        public IActionResult Review()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }
    }
}