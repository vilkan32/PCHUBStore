﻿using PCHUBStore.Data;
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

            var seeder = new SeedLaptops();
            //    seeder.SeedLaptopAsync(context).GetAwaiter().GetResult();
           seeder.SeedLaptopFiltersAsync(context).GetAwaiter().GetResult();

        }
    }
}