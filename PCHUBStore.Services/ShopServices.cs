using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
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
        private readonly IProductServices productService;

        public ShopServices(PCHUBDbContext context, IProductServices productService)
        {
            this.context = context;
            this.productService = productService;
        }
        public async Task BuyProductAsync(string userName, string productId)
        {
            if (await this.productService.ProductExistsAsync(productId))
            {
                var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == userName);

                var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == productId && x.IsDeleted == false);

                if (user.Cart == null)
                {
                    user.Cart = new Data.Models.ShoppingCart { CreatedOn = DateTime.UtcNow, ModificationDate = DateTime.UtcNow };
                }

                if (product != null)
                {
                    if (user.Cart.ProductCarts.Select(x => x.Product).Any(x => x.Id == productId))
                    {
                        user.Cart.ProductCarts.FirstOrDefault(x => x.ProductId == productId).Quantity += 1;
                    }
                    else
                    {
                        user.Cart.ProductCarts.Add(new Data.Models.ProductCart
                        {
                            Quantity = 1,
                            Product = product,
                        });
                    }

                    await this.context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<ProductCart>> GetAllCartProductsAsync(string username)
        {

            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            var products = user.Cart.ProductCarts.ToList();

            return products;
        }

        public async Task IncreaseProductQuantityAsync(string username, string productId, int quantity)
        {
            if (await this.productService.ProductExistsAsync(productId))
            {
                var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

                var cart = user.Cart;

                var productCart = cart.ProductCarts.FirstOrDefault(x => x.ProductId == productId);

                productCart.Quantity = quantity;

                await this.context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsCartEmptyOrNonExistingAsync(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if(user.Cart == null)
            {
                return true;
            }
            else
            {
                if(user.Cart.ProductCarts.Count == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<int> GetNumberOfProductsAsync(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            return user.Cart.ProductCarts.Sum(x => x.Quantity);
        }

        public async Task RemoveProductFromCartAsync(string username, string productId)
        {
            if(await this.productService.ProductExistsAsync(productId))
            {
                var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

                var productCart = await this.context.ProductCarts.FirstOrDefaultAsync(x => x.ProductId == productId);

                var cart = user.Cart.ProductCarts.Remove(productCart);

                await this.context.SaveChangesAsync();
            }
        }
    }
}
