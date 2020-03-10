using PCHUBStore.Data.Models;
using PCHUBStore.Filter.Models;
using PCHUBStore.View.Models;
using PCHUBStore.View.Models.FilterViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public interface ILaptopServices
    {
        Task<Product> GetLaptop(string id);
        Task<List<FilterCategory>> GetFilters(string category);
        Task<IEnumerable<Product>> QueryLaptops(LaptopFiltersUrlModel laptopFilters);
        Task OrderBy(ref LaptopsViewModel laptops, string args);
        Task ApplyFiltersFromUrl(ICollection<FilterCategoryViewModel> filterCategory, LaptopFiltersUrlModel urlData);
        Task<IEnumerable<Product>> GetSimilarLaptops(decimal currentPrice);
    }
}
