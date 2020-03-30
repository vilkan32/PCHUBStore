using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.Models;
using PCHUBStore.Services;
using PCHUBStore.Services.EmailSender;
using PCHUBStore.View.Models.ApiViewModels;
using PCHUBStore.View.Models.IndexViewModels;
using PCHUBStore.View.Models.MainSliderApiModels;

namespace PCHUBStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService service;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger,
            IHomeService service,
            IMapper mapper)
        {
            _logger = logger;
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
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

            return View(viewModel);
        }


        [HttpGet("/api/GetMainSliderPictures")]
        public async Task<ActionResult<List<MainSliderPicturesViewModel>>> MainSliderPictures()
        {

            var mainSliderPictures = await this.service.GetMainSliderPicturesAsync();

            var jsonModel = new List<MainSliderPicturesViewModel>();

            foreach (var item in mainSliderPictures)
            {
                jsonModel.Add(new MainSliderPicturesViewModel { Url = item.Url, Href = item.RedirectTo });
            }

            return jsonModel;
        }




        [HttpGet("/api/ReviewedProducts")]
        public async Task<ActionResult<List<ApiProductHistoryViewModel>>> ReviewedProducts()
        {

            var userReviews = await this.service.GetUserReviewedProductsAsync(this.User.Identity.Name);

            var history = this.mapper.Map<List<ApiProductHistoryViewModel>>(userReviews);

            return history;

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
