using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Areas.Support.Models;
using PCHUBStore.Areas.Support.Services;

namespace PCHUBStore.Areas.Support.Controllers
{
    public class ChartsController : SupportController
    {
        private readonly ISupportChartsService service;

        public ChartsController(ISupportChartsService service)
        {
            this.service = service;
        }

        public IActionResult Statistics()
        {       
            return View();
        }

        [HttpGet("/api/GetChartsViewModel")]
        public async Task<ChartsViewModel> GetChartsViewModel()
        {
            var model = new ChartsViewModel();


            model.MostViewedProducts = await this.service.GetMostViewedProductsAsync();
            model.MostExpensiveProducts = await this.service.GetMostExpensiveProductsAsync();

            return model;
        }
    }
}