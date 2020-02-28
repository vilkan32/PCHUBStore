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

namespace PCHUBStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly PCHUBDbContext context;
        public HomeController(ILogger<HomeController> logger, PCHUBDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index(string[] some)
        {
      

            var category = new Category { Name = "Laptops" };

            var product = new Product();

            var basicCharModel = new BasicCharacteristic();
            basicCharModel.Key = "Model";
            basicCharModel.Value = "Acer";

            var basicCharProcessor = new BasicCharacteristic();
            basicCharProcessor.Key = "Processor";
            basicCharProcessor.Value = "Intel Core i5";

            product.BasicCharacteristics.Add(basicCharModel);
            product.BasicCharacteristics.Add(basicCharProcessor);

            category.Products.Add(product);

            context.Categories.Add(category);

            context.SaveChanges();
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

    public class Tester
    {
        public List<string> some { get; set; }
    }
}
