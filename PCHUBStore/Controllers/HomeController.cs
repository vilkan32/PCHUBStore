using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.Models;
using PCHUBStore.Services;
using PCHUBStore.Services.EmailSender;
using PCHUBStore.View.Models.IndexViewModels;

namespace PCHUBStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender sender;
        private readonly IHomeService service;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger,
            IEmailSender sender,
            IHomeService service,
             IMapper mapper)
        {
            _logger = logger;
            this.sender = sender;
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var indexModel = await this.service.LoadIndexPageComponentsAsync();

            var viewModel = this.mapper.Map<IndexViewModel>(indexModel);

            // todo tova utre plus shopping card plus many to many na shipment administration page for crafting other categories 
          //  this.HttpContext.Response.Cookies.Append("shoppingCart", "productId", new CookieOptions { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires =DateTimeOffset.UtcNow.AddDays(20) }) ;

            // this.HttpContext.Response.Cookies.Re["asd"] = "asdasd";
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
