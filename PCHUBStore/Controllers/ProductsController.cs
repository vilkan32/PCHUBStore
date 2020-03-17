using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Filter.Models;
using PCHUBStore.MiddlewareFilters;
using PCHUBStore.Services;
using PCHUBStore.View.Models.FilterViewModels;
using PCHUBStore.View.Models;
using PCHUBStore.View.Models.Pagination;

namespace PCHUBStore.Controllers
{
    public class ProductsController : Controller
    {

        private readonly ILaptopServices service;

        private readonly IMapper mapper;
        public ProductsController(ILaptopServices service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }


        [TypeFilter(typeof(ValidationFilter))]
        [HttpGet("/Laptops")]
        public async Task<IActionResult> Laptops([FromQuery]LaptopFiltersUrlModel laptopFilters)
        {


            var laptopViewModel = new LaptopsViewModel();

            var laptops = await this.service.QueryLaptops(laptopFilters);

            var filters = await this.service.GetFilters("Laptops");

            laptopViewModel.FilterCategory = mapper.Map<List<FilterCategoryViewModel>>(filters);

            laptopViewModel.Laptops = mapper.Map<List<LaptopViewModel>>(laptops);
            
            await this.service.OrderBy(ref laptopViewModel, laptopFilters.OrderBy);

            laptopViewModel.Pager = new Pager(laptopViewModel.Laptops.Count, laptopFilters.Page, 20);

            laptopViewModel.Laptops = laptopViewModel.Laptops.Skip((laptopFilters.Page - 1) * 20).Take(20).ToList();

            await this.service.ApplyFiltersFromUrl(laptopViewModel.FilterCategory, laptopFilters);
            
            return this.View(laptopViewModel);
        }


        [HttpGet("Products/Laptop/{laptopId}")]
        public async Task<IActionResult> Laptop(string laptopId)
        {
            var laptop = await this.service.GetLaptop(laptopId, this.User.Identity.Name, this.User.Identity.IsAuthenticated);

            var laptopViewModel = mapper.Map<LaptopFullCharacteristicsViewModel>(laptop);

            var similarLaptops = await this.service.GetSimilarLaptops(laptop.Price);

            laptopViewModel.SimilarLaptops = mapper.Map<ICollection<SimilarLaptop>>(similarLaptops.Take(3));

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