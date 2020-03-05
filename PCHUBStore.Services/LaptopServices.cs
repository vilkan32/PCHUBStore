using Constants;
using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.Filter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if(parameter == "All")
            {
                Func<T, bool> funcResult = (x) => true;

                return funcResult;
            }
            else
            {
                Func<T, bool> funcResult = (x) => x.Key == parameter && x.Value.Contains(value);

                return funcResult;
            }

        }

        public async Task<List<FilterCategory>> GetFilters(string category)
        {
            return  await this.context.FilterCategories.Where(x => x.CategoryName == category).ToListAsync();
        }

        public async Task<IEnumerable<Product>> QueryLaptops(LaptopFiltersUrlModel laptopFilters)
        {

            decimal minPrice;
            decimal maxPrice;

            if(!decimal.TryParse(laptopFilters.MaxPrice, out maxPrice))
            {
                maxPrice = 30000;
            }
            if(!decimal.TryParse(laptopFilters.MinPrice, out minPrice))
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
            if(laptopFilters.VideoCard == null)
            {
                laptopFilters.VideoCard = new string[] { "All" };
            }

            var laptopCategory = await this.context.Categories
                .FirstAsync(x => x.Name == "Laptops");

            return laptopCategory
            .Products
            .Where(p =>
            laptopFilters.Model
            .Any(m => p.FullCharacteristics
            .Any(DetermineIfAll<FullCharacteristic>(LaptopFilterConstants.model, m)))
            &&
            laptopFilters.Processor
            .Any(pr => p.FullCharacteristics
            .Any(DetermineIfAll<FullCharacteristic>(LaptopFilterConstants.processor, pr)))
            &&
            p.Price >= minPrice && p.Price <= maxPrice
            &&
            p.IsDeleted == false
            &&
            laptopFilters.Make
            .Any(m => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(LaptopFilterConstants.make, m)))
            &&
            laptopFilters.VideoCard
            .Any(vc => p.BasicCharacteristics
            .Any(DetermineIfAll<BasicCharacteristic>(LaptopFilterConstants.processor, vc)))
                   ).ToList();

        }

        public async Task<Product> GetLaptop(string id)
        {
            var category = await this.context.Categories.FirstAsync(x => x.Name == "Laptops");

            var laptop = category.Products.FirstOrDefault(x => x.Id == id);

            return laptop;
        }

        public async Task<IEnumerable<Product>> GetAllLaptops()
        {

            var laptopCategory = await this.context.Categories.FirstAsync(x => x.Name == "Laptops");


            return laptopCategory.Products.ToList();
        }
    }
}
