using Constants;
using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.Filter.Models;
using PCHUBStore.View.Models;
using PCHUBStore.View.Models.FilterViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public class ProductServices : IProductServices
    {
        private readonly PCHUBDbContext context;

        public ProductServices(PCHUBDbContext context)
        {
            this.context = context;
        }

        private Func<T, bool> DetermineIfAll<T>(string value) where T : BaseCharacteristicsModel
        {


            if (value == "All")
            {
                Func<T, bool> funcResult = (x) => true;

                return funcResult;
            }
            else
            {
                Func<T, bool> funcResult = (x) => x.Value.ToLower().Contains(value.ToLower());

                return funcResult;
            }

        }

        public async Task<List<FilterCategory>> GetFiltersAsync(string category)
        {
            var filterCategory = await this.context.FilterCategories.Where(x => x.CategoryName == category && x.IsDeleted == false).ToListAsync();

            return filterCategory;
        }

        public Task ApplyFiltersFromUrlAsync(ICollection<FilterCategoryViewModel> filterCategory, ProductFiltersUrlModel urlData)
        {

            foreach (var category in filterCategory)
            {
                foreach (var filter in category.Filters)
                {
                    var properties = urlData.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                    foreach (var prop in properties)
                    {

                        if (prop.Name == filter.Name && prop.Name != "MinPrice" && prop.Name != "MaxPrice" && prop.Name != "Page" && prop.Name != "OrderBy")
                        {
                            var val = this.GetPropertyValue(urlData, prop.Name) as string[];

                            if (val.Any(x => x == filter.Value))
                            {
                                filter.IsChecked = true;
                                break;
                            }
                        }

                    }
                }

            }

            if (decimal.TryParse(urlData.MaxPrice, out decimal maxPrice) && decimal.TryParse(urlData.MinPrice, out decimal minPrice))
            {
                string maxPriceString = maxPrice.ToString();
                string minPriceString = minPrice.ToString();


                var priceFilters = filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Price");

                priceFilters.Filters.FirstOrDefault(x => x.Name == "MinPrice").Value = minPriceString;

                priceFilters.Filters.FirstOrDefault(x => x.Name == "MaxPrice").Value = maxPriceString;
            }

            var orderByCategory = filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Order By");


            if (orderByCategory != null)
            {

                var orderByFilters = orderByCategory.Filters;
                var orderBy = orderByFilters.FirstOrDefault(x => x.Value == urlData.OrderBy);
                orderBy.IsChecked = true;

            }
            return Task.CompletedTask;
        }


        public Task OrderByAsync(ref ProductsViewModel laptops, string args)
        {
            if (args == null)
            {
                args = "Default";
                return Task.CompletedTask;
            }

            if (args == "PriceAsc")
            {
                laptops.Products = laptops.Products.OrderBy(x => x.Price).ToList();
                return Task.CompletedTask;
            }
            else if (args == "PriceDesc")
            {
                laptops.Products = laptops.Products.OrderByDescending(x => x.Price).ToList();
                return Task.CompletedTask;
            }


            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Product>> QueryProductsAsync(ProductFiltersUrlModel productFilters, string category)
        {

            decimal minPrice;
            decimal maxPrice;

            if (!decimal.TryParse(productFilters.MaxPrice, out maxPrice))
            {
                maxPrice = 30000;
            }
            if (!decimal.TryParse(productFilters.MinPrice, out minPrice))
            {
                minPrice = 0;
            }


            if (productFilters.Make == null)
            {
                productFilters.Make = new string[] { "All" };
            }
            if (productFilters.Model == null)
            {
                productFilters.Model = new string[] { "All" };
            }
            if (productFilters.OrderBy == null)
            {
                productFilters.OrderBy = "Default";
            }
            if (productFilters.Processor == null)
            {
                productFilters.Processor = new string[] { "All" };
            }
            if (productFilters.VideoCard == null)
            {
                productFilters.VideoCard = new string[] { "All" };
            }

            var result = new List<Product>();

            if (category.ToLower() == "laptops")
            {
                result = await QueryLaptopsAsync(productFilters, minPrice, maxPrice);
            }
            else if (category.ToLower() == "keyboards")
            {
                // todo fix the query method
       
                result = await QueryKeyboardsAsync(productFilters, minPrice, maxPrice);
            }
            else if (category.ToLower() == "mice")
            {
                // todo fix the query method
                result = await QueryMiceAsync(productFilters, minPrice, maxPrice);
            }
            else if (category.ToLower() == "monitors")
            {
                // todo fix the query method
                result = await QueryMonitorsAsync(productFilters, minPrice, maxPrice);
            }
            else if (category.ToLower() == "computers")
            {

              // todo fix the query method
                result = await QueryComputersAsync(productFilters, minPrice, maxPrice);
            }

            return result;
        }

        private async Task<List<Product>> QueryLaptopsAsync(ProductFiltersUrlModel productFilters, decimal minPrice, decimal maxPrice)
        {
            var productCategory = await this.context.Categories
               .FirstAsync(x => x.Name.ToLower() == "laptops");

            var result = productCategory
            .Products
            .Where(p =>
            p.Model != null
            &&
            productFilters.Model.ToList().Any(x => p.Model.ToLower() == x.ToLower() || x == "All")
            &&
            productFilters.Processor
            .Any(pr => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(pr)))
            &&
            p.Price >= minPrice && p.Price <= maxPrice
            &&
            p.IsDeleted == false
            &&
            productFilters.Make.ToList().Any(x => p.Make.ToLower() == x.ToLower() || x == "All")
            &&
            productFilters.VideoCard
            .Any(vc => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(vc)))
                ).ToList();

            return result;
        }

        private async Task<List<Product>> QueryMonitorsAsync(ProductFiltersUrlModel productFilters, decimal minPrice, decimal maxPrice)
        {
            var productCategory = await this.context.Categories
              .FirstAsync(x => x.Name.ToLower() == "monitors");

            var result = productCategory
            .Products
            .Where(p =>
            productFilters.Processor
            .Any(pr => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(pr)))
            &&
            p.Price >= minPrice && p.Price <= maxPrice
            &&
            p.IsDeleted == false
            &&
            productFilters.Make.ToList().Any(x => p.Make.ToLower() == x.ToLower() || x == "All")
            &&
            productFilters.VideoCard
            .Any(vc => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(vc)))
                ).ToList();

            return result;
        }

        private async Task<List<Product>> QueryKeyboardsAsync(ProductFiltersUrlModel productFilters, decimal minPrice, decimal maxPrice)
        {
            var productCategory = await this.context.Categories
            .FirstAsync(x => x.Name.ToLower() == "keyboards");

            var result = productCategory
            .Products
            .Where(p =>
            productFilters.Processor
            .Any(pr => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(pr)))
            &&
            p.Price >= minPrice && p.Price <= maxPrice
            &&
            p.IsDeleted == false
            &&
            productFilters.Make.ToList().Any(x => p.Make.ToLower() == x.ToLower() || x == "All")
            &&
            productFilters.VideoCard
            .Any(vc => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(vc)))
                ).ToList();

            return result;
        }

        private async Task<List<Product>> QueryMiceAsync(ProductFiltersUrlModel productFilters, decimal minPrice, decimal maxPrice)
        {
            var productCategory = await this.context.Categories
            .FirstAsync(x => x.Name.ToLower() == "mice");

            var result = productCategory
            .Products
            .Where(p =>
            productFilters.Processor
            .Any(pr => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(pr)))
            &&
            p.Price >= minPrice && p.Price <= maxPrice
            &&
            p.IsDeleted == false
            &&
            productFilters.Make.ToList().Any(x => p.Make.ToLower() == x.ToLower() || x == "All")
            &&
            productFilters.VideoCard
            .Any(vc => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(vc)))
                ).ToList();

            return result;
        }

        private async Task<List<Product>> QueryComputersAsync(ProductFiltersUrlModel productFilters, decimal minPrice, decimal maxPrice)
        {
            var productCategory = await this.context.Categories
               .FirstAsync(x => x.Name.ToLower() == "computers");

            var result = productCategory
            .Products
            .Where(p =>
            productFilters.Processor
            .Any(pr => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(pr)))
            &&
            p.Price >= minPrice && p.Price <= maxPrice
            &&
            p.IsDeleted == false
            &&
            productFilters.Make.ToList().Any(x => p.Make.ToLower() == x.ToLower() || x == "All")
            &&
            productFilters.VideoCard
            .Any(vc => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(vc)))
                ).ToList();

            return result;
        }
        public async Task<Product> GetProductAsync(string id, string userId, bool isAuthenticated, string cat)
        {
            var category = await this.context.Categories.FirstAsync(x => x.Name == cat && x.IsDeleted == false);

            var product = category.Products.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            product.Views += 1;

            if (isAuthenticated)
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == userId);

                if (!user.ProductUserReviews.Select(x => x.Product).Any(x => x.Id == product.Id))
                {
                    user.ProductUserReviews.Add(new ProductUserReview
                    {
                        User = user,
                        Product = product

                    });
                }
            }

            await this.context.SaveChangesAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetSimilarProductsAsync(decimal currentPrice, string category)
        {
            var categoryDb = await this.context.Categories.FirstAsync(x => x.Name == category && x.IsDeleted == false);

            var similarLaptops = categoryDb.Products.Where(x => x.Price > currentPrice + 50 || x.Price > currentPrice - 400);

            return similarLaptops;
        }

        public async Task<bool> CategoryExistsAsync(string category)
        {
            return await this.context.Categories.AnyAsync(x => x.Name.ToLower() == category.ToLower());
        }

        private object GetPropertyValue(ProductFiltersUrlModel urlData, string propName)
        {
            var prop = urlData.GetType().GetProperty(propName);

            return prop.GetValue(urlData, null);
        }


    }
}
