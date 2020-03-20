using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PCHUBStore.Areas.Administration.Controllers
{
    public class IndexCategoryController : AdministrationController
    {
        public IActionResult Review()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}