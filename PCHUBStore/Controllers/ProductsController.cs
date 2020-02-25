using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Filter.Models;

using PCHUBStore.View.Models;
using PCHUBStore.View.Models.Pagination;

namespace PCHUBStore.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet("Laptops")]
        public async Task<IActionResult> Laptops(int? page, LaptopFilters laptopFilters)
        {

            var laptopViewModel = new LaptopsViewModel();
            laptopViewModel.Pager = new Pager(150, page, 5);
  
            //this.ProductsService.QueryLaptops(laptopFilters);
            return this.View(laptopViewModel);
        }

 

        /*  

        public async Task<IActionResult> Keyboards(KeyboardFilters laptopFilters)
        {

            return this.View();
        }

        public async Task<IActionResult> Monitors(MonitorFilters laptopFilters)
        {

            return this.View();
        }

        public async Task<IActionResult> Mice(MiceFilters laptopFilters)
        {

            return this.View("Products", ProductsViewModel);
        }
        */


    }
}