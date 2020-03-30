using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public interface IShopServices
    {
        Task BuyProductAsync(string userName, string productId);

        Task<List<ProductCart>> GetAllCartProductsAsync(string username);

        Task<bool> IsCartEmptyOrNonExistingAsync(string username);

        Task RemoveProductFromCartAsync(string username, string productId);

        Task IncreaseProductQuantityAsync(string username, string productId, int quantity);

        Task<int> GetNumberOfProductsAsync(string username);

    }
}
