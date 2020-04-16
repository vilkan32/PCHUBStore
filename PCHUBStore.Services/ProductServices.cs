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

        private Func<T, bool> DetermineIfAll<T>(string value, string key) where T : BaseCharacteristicsModel
        {


            if (value == "All")
            {
                Func<T, bool> funcResult = (x) => true;

                return funcResult;
            }
            else
            {
                Func<T, bool> funcResult = (x) => x.Value.ToLower().Contains(value.ToLower()) && x.Key == key;

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


        public async Task<bool> ProductExistsAsync(string productId)
        {
            return await this.context.Products.AnyAsync(x => x.Id == productId);
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
            if (productFilters.OS == null)
            {
                productFilters.OS = new string[] { "All" };
            }
            if (productFilters.RAM == null)
            {
                productFilters.RAM = new string[] { "All" };
            }
            if (productFilters.Resolution == null)
            {
                productFilters.Resolution = new string[] { "All" };
            }
            if (productFilters.FPS == null)
            {
                productFilters.FPS = new string[] { "All" };
            }
            if (productFilters.ReactionTime == null)
            {
                productFilters.ReactionTime = new string[] { "All" };
            }
            if (productFilters.MatrixType == null)
            {
                productFilters.MatrixType = new string[] { "All" };
            }
            if (productFilters.DisplaySize == null)
            {
                productFilters.DisplaySize = new string[] { "All" };
            }
            if (productFilters.Gaming == null)
            {
                productFilters.Gaming = new string[] { "All" };
            }
            if (productFilters.Interface == null)
            {
                productFilters.Interface = new string[] { "All" };
            }
            if (productFilters.Connectivity == null)
            {
                productFilters.Connectivity = new string[] { "All" };
            }
            if (productFilters.Type == null)
            {
                productFilters.Type = new string[] { "All" };
            }
            if (productFilters.Mechanical == null)
            {
                productFilters.Mechanical = new string[] { "All" };
            }

            var result = new List<Product>();

            if (category.ToLower() == "laptops")
            {
                result = await QueryLaptopsAsync(productFilters, minPrice, maxPrice);
            }
            else if (category.ToLower() == "keyboards")
            {
                result = await QueryKeyboardsAsync(productFilters, minPrice, maxPrice);
            }
            else if (category.ToLower() == "mice")
            {
                result = await QueryMiceAsync(productFilters, minPrice, maxPrice);
            }
            else if (category.ToLower() == "monitors")
            {
                result = await QueryMonitorsAsync(productFilters, minPrice, maxPrice);
            }
            else if (category.ToLower() == "computers")
            {
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
            .Any(DetermineIfAll<BasicCharacteristic>(pr, "Processor")))
            &&
            p.Price >= minPrice && p.Price <= maxPrice
            &&
            p.IsDeleted == false
            &&
            productFilters.Make.ToList().Any(x => p.Make.ToLower() == x.ToLower() || x == "All")
            &&
            productFilters.VideoCard
            .Any(vc => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(vc, "VideoCard")))
            &&
            productFilters.OS
            .Any(os => p.FullCharacteristics
            .Any(DetermineIfAll<FullCharacteristic>(os, "OS")))
            &&
               productFilters.RAM
            .Any(ram => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(ram, "Ram")))
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
            productFilters.Resolution
            .Any(pr => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(pr, "Resolution")))
            &&
            p.Price >= minPrice && p.Price <= maxPrice
            &&
            p.IsDeleted == false
            &&
            productFilters.Make.ToList().Any(x => p.Make.ToLower() == x.ToLower() || x == "All")
            &&
            productFilters.FPS
            .Any(fps => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(fps, "FPS")))
            &&
            productFilters.ReactionTime
            .Any(rt => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(rt, "Reaction Time")))
            &&
             productFilters.MatrixType
            .Any(mt => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(mt, "Matrix Type")))
            &&
             productFilters.DisplaySize
            .Any(ds => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(ds, "Display Size")))
            )
            .ToList();

            return result;
        }

        private async Task<List<Product>> QueryKeyboardsAsync(ProductFiltersUrlModel productFilters, decimal minPrice, decimal maxPrice)
        {
            var productCategory = await this.context.Categories
            .FirstAsync(x => x.Name.ToLower() == "keyboards");

            var result = productCategory
                .Products
                .Where(p =>
            productFilters.Type
            .Any(t => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(t, "Type")))
            &&
            p.Price >= minPrice && p.Price <= maxPrice
            &&
            p.IsDeleted == false
            &&
            p.Make != null
            &&
            productFilters.Make.Any(x => p.Make.ToLower() == x.ToLower() || x == "All")
            &&
            productFilters.Interface
            .Any(i => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(i, "Interface")))
            &&
            productFilters.Mechanical
            .Any(m => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(m, "Mechanical"))))
            .ToList();

            return result;
        }

        private async Task<List<Product>> QueryMiceAsync(ProductFiltersUrlModel productFilters, decimal minPrice, decimal maxPrice)
        {
            var productCategory = await this.context.Categories
            .FirstAsync(x => x.Name.ToLower() == "mice");
            var result = productCategory
            .Products
            .Where(p =>
            productFilters.Gaming
            .Any(g => p.FullCharacteristics
            .Any(DetermineIfAll<FullCharacteristic>(g, "Gaming")))
            &&
            p.Price >= minPrice && p.Price <= maxPrice
            &&
            p.IsDeleted == false
            &&
            p.Make != null
            &&
            productFilters.Make.ToList().Any(x => p.Make.ToLower() == x.ToLower() || x == "All")
            &&
            productFilters.Connectivity
            .Any(c => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(c, "Connectivity")))
            &&
            productFilters.Interface
            .Any(i => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(i, "Interface"))))
            .ToList();

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
            .Any(DetermineIfAll<BasicCharacteristic>(pr, "Processor")))
            &&
            p.Price >= minPrice && p.Price <= maxPrice
            &&
            p.IsDeleted == false
            &&
            productFilters.VideoCard
            .Any(vc => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(vc, "VideoCard")))
            &&
            productFilters.OS
            .Any(os => p.FullCharacteristics
            .Any(DetermineIfAll<FullCharacteristic>(os, "OS")))
            &&
               productFilters.RAM
            .Any(ram => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(ram, "Ram"))))
             .ToList();

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
                await this.context.SaveChangesAsync();
            }

            

            return product;
        }

        public async Task<Product> GetProductAsync(string id)
        {
            return await this.context.Products.FirstOrDefaultAsync(x => x.Id == id );
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

        public async Task<IEnumerable<Product>> SearchForResultsAsync(string searchInput, string minPrice, string maxPrice)
        {
            decimal minPriceDecimal = 0;
            decimal.TryParse(minPrice, out minPriceDecimal);
            decimal maxPriceDecimal = 0;
            decimal.TryParse(maxPrice, out maxPriceDecimal);
            return await this.context.Products.Where(x => (x.Title.ToLower().Contains(searchInput.ToLower()) || x.ArticleNumber.ToLower().Contains(searchInput.ToLower())) && x.Price >= minPriceDecimal && x.Price <= maxPriceDecimal).ToListAsync();
        }

        private object GetPropertyValue(ProductFiltersUrlModel urlData, string propName)
        {
            var prop = urlData.GetType().GetProperty(propName);

            return prop.GetValue(urlData, null);
        }


    }
}
