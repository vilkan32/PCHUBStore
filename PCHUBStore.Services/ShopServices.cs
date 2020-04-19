using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.Data.Models.Enums;
using PCHUBStore.View.Models.ShoppingCartViewModels;
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
                if (user == null)
                {
                    throw new NullReferenceException();
                }
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

            if (user.Cart == null)
            {
                return true;
            }
            else
            {
                if (user.Cart.ProductCarts.Count == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<int> GetNumberOfProductsAsync(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if(user.Cart == null)
            {
                return 0;
            }
            else
            {
                return user.Cart.ProductCarts.Sum(x => x.Quantity);
            }
            
        }

        public async Task RemoveProductFromCartAsync(string username, string productId)
        {
            if (await this.productService.ProductExistsAsync(productId))
            {
                var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

                var productCart = await this.context.ProductCarts.FirstOrDefaultAsync(x => x.ProductId == productId);

                var cart = user.Cart.ProductCarts.Remove(productCart);

                await this.context.SaveChangesAsync();
            }
        }

        public async Task<bool> CheckoutSignedInUserAsync(string username, ShippingCompany shippingCompany)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if(user.FirstName == null || user.LastName == null || user.PhoneNumber == null || user.Address == null)
            {
                return false;
            }

            var userCartProducts = user.Cart.ProductCarts;

            var shipmentProducts = new List<ShipmentProduct>();

            foreach (var cartProduct in userCartProducts)
            {
                shipmentProducts.Add(new ShipmentProduct { Product = cartProduct.Product, Quantity = cartProduct.Quantity});
            }
            
            var shipment = new Shipment
            {

                ClientResponse = Data.Models.Enums.ClientResponse.AwaitingResponse,
                ConfirmationStatus = Data.Models.Enums.ConfirmationStatus.AwaitingResponse,
                PurchaseDate = DateTime.UtcNow,
                ShipmentProducts = shipmentProducts,
                ShippingCompany = shippingCompany,
                ShipmentDetails = ShipmentDetails.Send,
                ShipmentImportancy = ShipmentImportancy.Normal,
                ShipmentStatus = ShipmentStatus.Confirmed,
            };

            user.Shipments.Add(shipment);

            await this.context.SaveChangesAsync();

            var userNext = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            userNext.Cart = new ShoppingCart();

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task ChechoutAnonymousAsync(AnonymousCartViewModel form)
        {
            var user = new User { 

                FirstName = form.FirstName, 
                            
                LastName = form.LastName,

                Email = form.Email,

                City = form.City,

                PhoneNumber = form.PhoneNumber,

                Address = form.Address
            
            };


            var userCartProducts = form.Products;

            var shipmentProducts = new List<ShipmentProduct>();

            foreach (var cartProduct in userCartProducts)
            {
                shipmentProducts.Add(new ShipmentProduct { ProductId = cartProduct.Id, Quantity = cartProduct.Quantity });
            }

            var shipment = new Shipment
            {

                ClientResponse = Data.Models.Enums.ClientResponse.AwaitingResponse,
                ConfirmationStatus = Data.Models.Enums.ConfirmationStatus.AwaitingResponse,
                PurchaseDate = DateTime.UtcNow,
                ShipmentProducts = shipmentProducts,
                ShippingCompany = (ShippingCompany)Enum.Parse(typeof(ShippingCompany), form.ShippingCompany.ToString("g")),
                ShipmentDetails = ShipmentDetails.Send,
            };

            user.Shipments.Add(shipment);

            await this.context.Users.AddAsync(user);

            await this.context.SaveChangesAsync();
        }

        public async Task<AnonymousCartViewModel> GetLastCheckoutDetailsAsync(string username)
        {

            var result = new AnonymousCartViewModel();

            result.Products = new List<PurchaseProductsAnonymousViewModel>();

            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            var lastShipment = user.Shipments.OrderByDescending(x => x.PurchaseDate).ToList().Take(1);

            foreach (var shipment in lastShipment)
            {
                foreach (var productShipment in shipment.ShipmentProducts)
                {
                    var product = productShipment.Product;
                    result.Products.Add(new PurchaseProductsAnonymousViewModel
                    {
                        Id = shipment.Id.ToString(),
                        PictureUrl = product.MainPicture.Url,
                        Price = product.Price * productShipment.Quantity,
                        Title = product.Title,
                        Quantity = productShipment.Quantity,
                    });
                }
            }

            result.FirstName = user.FirstName;
            result.LastName = user.LastName;
            result.PhoneNumber = user.PhoneNumber;
            result.Address = user.Address;
            result.Email = user.Email;
            result.City = user.City;

            return result;

        }
    }
     
}
