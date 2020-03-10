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
    public class LaptopServices : ILaptopServices
    {
        private readonly PCHUBDbContext context;

        public LaptopServices(PCHUBDbContext context)
        {
            this.context = context;
        }

        private Func<T, bool> DetermineIfAll<T>(string parameter, string value) where T : BaseCharacteristicsModel
        {


            if (value == "All")
            {
                Func<T, bool> funcResult = (x) => true;

                return funcResult;
            }
            else
            {
                Func<T, bool> funcResult = (x) => x.Key.ToLower() == parameter.ToLower() && x.Value.ToLower().Contains(value.ToLower());

                return funcResult;
            }

        }

        public async Task<List<FilterCategory>> GetFilters(string category)
        {
            var filterCategory = await this.context.FilterCategories.Where(x => x.CategoryName == category && x.IsDeleted == false).ToListAsync();

            return filterCategory;
        }

        public Task ApplyFiltersFromUrl(ICollection<FilterCategoryViewModel> filterCategory, LaptopFiltersUrlModel urlData)
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


                filterCategory.Add(new FilterCategoryViewModel
                {
                    CategoryName = "Laptops",
                    ViewSubCategoryName = "Price",
                    Filters = new List<FilterViewModel>
                    {
                       new FilterViewModel
                       {
                            Name = "MinPrice",
                            Value = minPriceString
                       },
                       new FilterViewModel
                       {
                           Name = "MaxPrice",
                           Value = maxPriceString
                       }
                    }

                });
            }

            var orderByCategory = filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "OrderBy");

            var orderByFilters = orderByCategory.Filters;

            var orderBy = orderByFilters.FirstOrDefault(x => x.Value == urlData.OrderBy);

            orderBy.IsChecked = true;

            return Task.CompletedTask;
        }


        public Task OrderBy(ref LaptopsViewModel laptops, string args)
        {
            if(args == null)
            {
                args = "Default";
                return Task.CompletedTask;
            }

            if(args == "PriceAsc")
            {
                laptops.Laptops = laptops.Laptops.OrderBy(x => x.Price).ToList();
                return Task.CompletedTask;
            }
            else if(args == "PriceDesc")
            {
                laptops.Laptops = laptops.Laptops.OrderByDescending(x => x.Price).ToList();
                return Task.CompletedTask;
            }


            return Task.CompletedTask;
        } 

        public async Task<IEnumerable<Product>> QueryLaptops(LaptopFiltersUrlModel laptopFilters)
        {

            decimal minPrice;
            decimal maxPrice;

            if (!decimal.TryParse(laptopFilters.MaxPrice, out maxPrice))
            {
                maxPrice = 30000;
            }
            if (!decimal.TryParse(laptopFilters.MinPrice, out minPrice))
            {
                minPrice = 400;
            }


            if (laptopFilters.Make == null)
            {
                laptopFilters.Make = new string[] { "All" };
            }
            if (laptopFilters.Model == null)
            {
                laptopFilters.Model = new string[] { "All" };
            }
            if (laptopFilters.OrderBy == null)
            {
                laptopFilters.OrderBy = "Default";
            }
            if (laptopFilters.Processor == null)
            {
                laptopFilters.Processor = new string[] { "All" };
            }
            if (laptopFilters.VideoCard == null)
            {
                laptopFilters.VideoCard = new string[] { "All" };
            }

            var laptopCategory = await this.context.Categories
                .FirstAsync(x => x.Name == "Laptops");

            var asd = laptopFilters.Model.Any(x => x == "All");

            var result = laptopCategory
            .Products
            .Where(p =>
            p.Model != null & p.Model != null
            &&
            laptopFilters.Model.ToList().Any(x => p.Model.ToLower() == x.ToLower() || x == "All")
            &&
            laptopFilters.Processor
            .Any(pr => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(LaptopFilterConstants.processor, pr)))
            &&
            p.Price >= minPrice && p.Price <= maxPrice
            &&
            p.IsDeleted == false
            &&
            laptopFilters.Make.ToList().Any(x => p.Make.ToLower() == x.ToLower() || x == "All")
            &&
            laptopFilters.VideoCard
            .Any(vc => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(LaptopFilterConstants.videoCard, vc)))
                   ).ToList();

            return result;
        }

        public async Task<Product> GetLaptop(string id)
        {
            var category = await this.context.Categories.FirstAsync(x => x.Name == "Laptops" && x.IsDeleted == false);

            var laptop = category.Products.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            return laptop;
        }

        public async Task<IEnumerable<Product>> GetSimilarLaptops(decimal currentPrice)
        {
            var category = await this.context.Categories.FirstAsync(x => x.Name == "Laptops" && x.IsDeleted == false);

            var similarLaptops = category.Products.Where(x => x.Price > currentPrice + 50 || x.Price > currentPrice - 400);

            return similarLaptops;
        }

        public object GetPropertyValue(LaptopFiltersUrlModel urlData, string propName)
        {
            var prop = urlData.GetType().GetProperty(propName);

            return prop.GetValue(urlData, null);
        }

    }
}
