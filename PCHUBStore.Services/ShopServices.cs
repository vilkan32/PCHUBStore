using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.Services
{
    public class ShopServices : IShopServices
    {
        private readonly PCHUBDbContext context;

        public ShopServices(PCHUBDbContext context)
        {
            this.context = context;
        }
        public async Task BuyProductAsync(string userName, string productId)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == productId && x.IsDeleted == false);

            if (user.Cart == null)
            {
                user.Cart = new Data.Models.ShoppingCart { CreatedOn = DateTime.UtcNow, ModificationDate = DateTime.UtcNow };
            }

            if (product != null && !user.Cart.ProductCarts.Select(x => x.Product).Any(x => x.Id == productId))
            {
                if (user.Cart.ProductCarts.Select(x => x.Product).Any(x => x.Id == productId))
                {
                    user.Cart.ProductCarts.FirstOrDefault(x => x.ProductId == productId).Quantity += 1;
                }
                else
                {
                    user.Cart.ProductCarts.Add(new Data.Models.ProductCart
                    {
                        Product = product
                    });
                }

                await this.context.SaveChangesAsync();
            }
        }
    }
}
