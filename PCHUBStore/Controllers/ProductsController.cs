using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PCHUBStore.Filter.Models;
using PCHUBStore.MiddlewareFilters;
using PCHUBStore.View.Models.FilterViewModels;
using PCHUBStore.View.Models;
using PCHUBStore.View.Models.Pagination;
using PCHUBStore.Services;
using System.ComponentModel.DataAnnotations;
using Ganss.XSS;

namespace PCHUBStore.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProductServices service;

        private readonly IMapper mapper;


        public ProductsController(IProductServices service,
            IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }


        [TypeFilter(typeof(ValidationFilter))]
        [HttpGet("/Products/{category}")]
        public async Task<IActionResult> Products([FromQuery]ProductFiltersUrlModel productFilters, string category)
        {
            var categoryExists = await this.service.CategoryExistsAsync(category);
            
            if (!categoryExists)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var productViewModel = new ProductsViewModel();

            var products = await this.service.QueryProductsAsync(productFilters, category);
            
            var filters = await this.service.GetFiltersAsync(category);

            productViewModel.FilterCategory = mapper.Map<List<FilterCategoryViewModel>>(filters);

            productViewModel.Products = mapper.Map<List<ProductViewModel>>(products);
            
            await this.service.OrderByAsync(ref productViewModel, productFilters.OrderBy);

            productViewModel.Pager = new Pager(productViewModel.Products.Count, productFilters.Page, 20);

            productViewModel.Products = productViewModel.Products.Skip((productFilters.Page - 1) * 20).Take(20).ToList();

            productViewModel.Category = category;

            await this.service.ApplyFiltersFromUrlAsync(productViewModel.FilterCategory, productFilters);
            
            return this.View(productViewModel);
        }

        [HttpGet("Products/{category}/{productId}")]
        public async Task<IActionResult> Product(string productId, string category)
        {
            var categoryExists = await this.service.CategoryExistsAsync(category);

            if(!await this.service.ProductExistsAsync(productId))
            {
                return this.RedirectToAction("Error", "Home");
            }

            var product = await this.service.GetProductAsync(productId, this.User.Identity.Name, this.User.Identity.IsAuthenticated, category);

            if (!categoryExists || product.Category.Name != category)
            {
                return this.Redirect("Home/Error");
            }
   
            var productViewModel = mapper.Map<ProductFullCharacteristicsViewModel>(product);

            var similarProducts = await this.service.GetSimilarProductsAsync(product.Price, category);

            productViewModel.SimilarProducts = mapper.Map<ICollection<SimilarProduct>>(similarProducts.Take(3));

            return this.View(productViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search([Required][MinLength(1)]string searchInput, [FromQuery]ProductFiltersUrlModel productFilters)
        {
            if (productFilters.OrderBy == null)
            {
                productFilters.OrderBy = "Default";
            }
            if(productFilters.MinPrice == null)
            {
                productFilters.MinPrice = "0";
            }
            if(productFilters.MaxPrice == null)
            {
                productFilters.MaxPrice = "9999";
            }
 
            var model = new ProductsViewModel();

            var products = await this.service.SearchForResultsAsync(searchInput, productFilters.MinPrice, productFilters.MaxPrice);

            var filters = await this.service.GetFiltersAsync("Search");

            model.FilterCategory = mapper.Map<List<FilterCategoryViewModel>>(filters);

            model.Products = mapper.Map<List<ProductViewModel>>(products);

            model.Pager = new Pager(model.Products.Count, productFilters.Page, 20);

            model.Products = model.Products.Skip((productFilters.Page - 1) * 20).Take(20).ToList();

            await this.service.OrderByAsync(ref model, productFilters.OrderBy);

            model.Category = "Search";

            model.Search = true;

            model.SearchInput = searchInput;

            await this.service.ApplyFiltersFromUrlAsync(model.FilterCategory, productFilters);

            return this.View("Products",model);
        }
    }
}