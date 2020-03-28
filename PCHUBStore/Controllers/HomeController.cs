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
        private readonly IHomeService service;

        public HomeController(ILogger<HomeController> logger,
            IHomeService service)
        {
            _logger = logger;
            this.service = service;
        }

        public  async Task<IActionResult> Index()
        {
            var page = await this.service.LoadIndexPageComponentsAsync();

            var viewModel = new IndexViewModel();

            foreach (var item in page.ColorfulBoxes.Where(x => x.IsDeleted == false))
            {
                viewModel.Boxes.Add(new BoxViewModel { Color = item.Color, Href = item.Href, Text = item.Text });                
            }


            foreach (var cat in page.Categories)
            {
                var categories = new List<IndexItemCategoryViewModel>();

                foreach (var ic in cat.ItemsCategories)
                {
                    var items = new List<IndexCategoryItemViewModel>();

                    foreach (var item in ic.Items)
                    {
                        items.Add(new IndexCategoryItemViewModel
                        {

                            Text = item.Text,
                            Href = item.Href

                        });
                    }

                    categories.Add(new IndexItemCategoryViewModel { ItemCategory = ic.Category, Items = items });


                }

                viewModel.Categories.Add(new IndexCategoryViewModel
                {
                    CategoryName = cat.CategoryName,
                    AllHref = cat.AllHref,
                    AllName = cat.AllName,
                    PictureUrl = cat.Pictures.FirstOrDefault().Url,
                    ItemCategories = categories
                });
            }


            // todo tova utre plus shopping card plus many to many na shipment administration page for crafting other categories 
          //  this.HttpContext.Response.Cookies.Append("shoppingCart", "productId", new CookieOptions { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires =DateTimeOffset.UtcNow.AddDays(20) }) ;

            // this.HttpContext.Response.Cookies.Re["asd"] = "asdasd";
            return View(viewModel);
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
