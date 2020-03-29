using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public interface IShopServices
    {
        Task BuyProductAsync(string userName, string productId);
    }
}
