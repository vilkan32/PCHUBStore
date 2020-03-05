﻿using System;
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

/*
        [HttpGet("Products/Laptops/All")]
        public async Task<IActionResult> LaptopsAll([FromQuery]int? page)
        {

            var laptopsViewModel = new LaptopsViewModel();
            laptopsViewModel.Pager = new Pager(150, page, 5);
            var result = await service.GetAllLaptops();

            var laptops = mapper.Map<List<LaptopViewModel>>(result);


            laptopsViewModel.Laptops = laptops;
            return this.View("Laptops");
        }

 * */
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
            Console.WriteLine();

            var laptopViewModel = new LaptopsViewModel();

            laptopViewModel.Pager = new Pager(60, laptopFilters.Page, 20);
 
            var laptops = await this.service.GetAllLaptops();

            var filters = await this.service.GetFilters("Laptops");

            laptopViewModel.FilterCategory = mapper.Map<List<FilterCategoryViewModel>>(filters);

            laptopViewModel.Laptops = mapper.Map<List<LaptopViewModel>>(laptops);
            return this.View(laptopViewModel);
        }



        public async Task<IActionResult> Laptop([FromQuery]string laptopId)
        {
            await this.service.GetLaptop(laptopId);

            return this.View();
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