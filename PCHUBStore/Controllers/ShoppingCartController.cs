using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PCHUBStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        [HttpGet]
        public IActionResult ReviewCart()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult ExcludeProduct()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult BuyProducts()
        {
            return this.View();
        }


    }
}