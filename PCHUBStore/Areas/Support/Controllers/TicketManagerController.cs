using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PCHUBStore.Areas.Support.Controllers
{
    public class TicketManagerController : SupportController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}