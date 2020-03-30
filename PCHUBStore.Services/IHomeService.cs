using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public interface IHomeService
    {
        Task<Page> LoadIndexPageComponentsAsync();

        Task<List<Picture>> GetMainSliderPicturesAsync();

        Task<IEnumerable<Product>> GetUserReviewedProductsAsync(string username);
    }
}
