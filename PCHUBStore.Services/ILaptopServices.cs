using PCHUBStore.Data.Models;
using PCHUBStore.Filter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public interface ILaptopServices
    {
        Task<Product> GetLaptop(string id);

        Task<IEnumerable<Product>> GetAllLaptops();

        Task<List<FilterCategory>> GetFilters(string category);
        Task<IEnumerable<Product>> QueryLaptops(LaptopFiltersUrlModel laptopFilters);
    }
}
