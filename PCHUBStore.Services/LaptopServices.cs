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
    public class LaptopServices
    {


        private readonly PCHUBDbContext context;


        public LaptopServices(PCHUBDbContext context)
        {
            this.context = context;
        }

        private Func<BasicCharacteristic, bool> DetermineIfAll(string parameter, string value)
        {
            if(parameter == "All")
            {
                Func<BasicCharacteristic, bool> funcResult = (x) => true;

                return funcResult;
            }
            else
            {
                Func<BasicCharacteristic, bool> funcResult = (x) => x.Key == parameter && x.Value.Contains(value);

                return funcResult;
            }

        }


        public async Task<IEnumerable<Product>> QueryLaptops(LaptopFilters laptopFilters)
        {


            // todo validate filters and add for all option create predicates
 

            var laptopCategory = await this.context.Categories
                .FirstAsync(x => x.Name == "Laptops");

            return laptopCategory
            .Products
            .Where(p =>
            laptopFilters.Model.Any(m => p.BasicCharacteristics.Any(DetermineIfAll("Model", m)))
            &&
            laptopFilters.Processor.Any(pr => p.BasicCharacteristics.Any(DetermineIfAll("Processor", pr)))

                   ).ToList();

        }


        public async Task<IEnumerable<Product>> GetAllLaptops()
        {

            var laptopCategory = await this.context.Categories.FirstAsync(x => x.Name == "Laptops");


            return laptopCategory.Products.ToList();
        }
    }
}
