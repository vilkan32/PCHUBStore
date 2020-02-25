using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.Filter.Models;
using PCHUBStore.Services;
using System;

namespace PCHUBStore.DemoTestEnvironment
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new PCHUBDbContext();
            var service = new LaptopServices(context);
            var filter = new LaptopFilters();
            filter.Model.Add("Acer");
            filter.Processor.Add("Intel Core i5");
            Console.WriteLine();
            var result = service.QueryLaptops(filter).GetAwaiter().GetResult();
            Console.WriteLine();
            var allLaptops = service.GetAllLaptops().GetAwaiter().GetResult();

        }
    }
}
