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
    public interface IProductServices
    {
        Task<Product> GetProductAsync(string id, string userId, bool isAuthenticated, string category);
        Task<List<FilterCategory>> GetFiltersAsync(string category);
        Task<IEnumerable<Product>> SearchForResultsAsync(string searchInput, string minPrice, string maxPrice);
        Task<IEnumerable<Product>> QueryProductsAsync(ProductFiltersUrlModel productFilters, string category);
        Task OrderByAsync(ref ProductsViewModel products, string args);
        Task ApplyFiltersFromUrlAsync(ICollection<FilterCategoryViewModel> filterCategory, ProductFiltersUrlModel urlData);
        Task<IEnumerable<Product>> GetSimilarProductsAsync(decimal currentPrice, string category);
        Task<bool> CategoryExistsAsync(string category);
        Task<bool> ProductExistsAsync(string productId);
        Task<Product> GetProductAsync(string id);
    }
}
