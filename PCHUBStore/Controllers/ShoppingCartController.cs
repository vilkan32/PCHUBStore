using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PCHUBStore.Data.Models.Enums;
using PCHUBStore.MiddlewareFilters;
using PCHUBStore.Models;
using PCHUBStore.Services;
using PCHUBStore.View.Models.ShoppingCartViewModels;

namespace PCHUBStore.Controllers
{
    [Authorize(Roles = "StoreUser")]
    public class ShoppingCartController : Controller
    {
        private readonly IShopServices service;
        private readonly IMapper mapper;
        private readonly IProductServices productService;

        public ShoppingCartController(IShopServices service, IMapper mapper, IProductServices productService)
        {
            this.service = service;
            this.mapper = mapper;
            this.productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> ReviewCart()
        {

            var emptyOrNonExistingCart = await this.service.IsCartEmptyOrNonExistingAsync(this.User.Identity.Name);

            if (emptyOrNonExistingCart)
            {
                return this.RedirectToAction("EmptyCart");
            }


            var cartProducts = await this.service.GetAllCartProductsAsync(this.User.Identity.Name);

            var model = this.mapper.Map<List<ReviewCartViewModel>>(cartProducts);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ExcludeProduct(string id)
        {
            var emptyOrNonExistingCart = await this.service.IsCartEmptyOrNonExistingAsync(this.User.Identity.Name);

            if (emptyOrNonExistingCart)
            {
                return this.RedirectToAction("EmptyCart");
            }


            await this.service.RemoveProductFromCartAsync(this.User.Identity.Name, id);

            return this.RedirectToAction("ReviewCart");
        }



        [HttpGet]
        public async Task<IActionResult> BuyProduct([Required][MaxLength(80)]string id)
        {
            await this.service.BuyProductAsync(this.User.Identity.Name, id);

            return this.RedirectToAction("ReviewCart");
        }

        [HttpGet()]
        public async Task<IActionResult> IncreaseProductQuantity(string id, int quantity)
        {
            var emptyOrNonExistingCart = await this.service.IsCartEmptyOrNonExistingAsync(this.User.Identity.Name);

            if (emptyOrNonExistingCart)
            {
                return this.RedirectToAction("EmptyCart");
            }

            if (!(quantity < 1))
            {
                await this.service.IncreaseProductQuantityAsync(this.User.Identity.Name, id, quantity);
            }


            return this.RedirectToAction("ReviewCart");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(ShippingCompany shippingCompany)
        {


            if(await this.service.CheckoutSignedInUserAsync(this.User.Identity.Name, shippingCompany))
            {
                return this.RedirectToAction("CheckoutDetails");
            }
            else
            {
                return this.Redirect("/User/Profile");
            }


           
        }

        // SharedPart

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> EmptyCart()
        {

            if (this.User.Identity.IsAuthenticated)
            {
                var emptyOrNonExistingCart = await this.service.IsCartEmptyOrNonExistingAsync(this.User.Identity.Name);

                if (emptyOrNonExistingCart)
                {
                    return this.View();
                }
            }
            else
            {
                if (this.HttpContext.Session.GetString("Cart") == "empty")
                {

                    return this.View();
                }
                else
                {
                    var cookiesJson = this.HttpContext.Session.GetString("Cart");

                    var model = JsonConvert.DeserializeObject<CartCookieModel>(cookiesJson);

                    var quantity = model.Products.Sum(x => x.Quantity);

                    if(quantity == 0)
                    {
                        return this.View();
                    }
                }
            }


            return this.RedirectToAction("Error", "Home");
        }

        [AllowAnonymous]
        [ShoppingCartFilter]
        [HttpGet]
        [Route("/api/NumberOfProducts")]
        public async Task<int> NumberOfProducts()
        {

            if (this.User.IsInRole("StoreUser"))
            {
                return await this.service.GetNumberOfProductsAsync(this.User.Identity.Name);
            }
            else
            {
                if (this.HttpContext.Session.GetString("Cart") == "empty")
                {

                    return 0;
                }
                else
                {
                    var cookiesJson = this.HttpContext.Session.GetString("Cart");

                    var model = JsonConvert.DeserializeObject<CartCookieModel>(cookiesJson);
                    return model.Products.Sum(x => x.Quantity);
                }
            }

        }

        // SharedPart


        // Anonymous Part
        [AllowAnonymous]
        [ShoppingCartFilter]
        [HttpGet]
        public async Task<IActionResult> BuyProductAnonymous([Required][MaxLength(80)]string id)
        {

            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (this.HttpContext.Session.GetString("Cart") == "empty")
            {
                var product = await this.productService.GetProductAsync(id);

                var model = new CartCookieModel();

                var cookieProduct = this.mapper.Map<ProductCookieModel>(product);

                cookieProduct.Quantity = 1;

                model.Products.Add(cookieProduct);

                var jsonModel = JsonConvert.SerializeObject(model);

                this.HttpContext.Session.SetString("Cart", jsonModel);
            }
            else
            {

                var product = await this.productService.GetProductAsync(id);

                var cookieProduct = this.mapper.Map<ProductCookieModel>(product);

                var model = JsonConvert.DeserializeObject<CartCookieModel>(this.HttpContext.Session.GetString("Cart"));

                if (model.Products.Any(x => x.ProductId == id))
                {
                    var productModel = model.Products.FirstOrDefault(x => x.ProductId == id);

                    productModel.Quantity += 1;

                }
                else
                {
                    cookieProduct.Quantity = 1;
                    model.Products.Add(cookieProduct);
                    
                }
                var jsonModel = JsonConvert.SerializeObject(model);

                this.HttpContext.Session.SetString("Cart", jsonModel);
            }

            return this.RedirectToAction("ReviewCartAnonymous");
        }


        [AllowAnonymous]
        [HttpGet("/Cart")]
        [ShoppingCartFilter]
        public async Task<IActionResult> ReviewCartAnonymous()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (this.HttpContext.Session.GetString("Cart") == "empty")
            {
                return this.RedirectToAction("EmptyCart");
            }

            var cookiesJson = this.HttpContext.Session.GetString("Cart");


            var cookieModel = JsonConvert.DeserializeObject<CartCookieModel>(cookiesJson);

            var quantity = cookieModel.Products.Sum(x => x.Quantity);

            if(quantity == 0)
            {
                return this.RedirectToAction("EmptyCart");
            }

            var model = this.mapper.Map<AnonymousCartViewModel>(cookieModel);

            if(model.Products == null)
            {
                model.Products = new List<PurchaseProductsAnonymousViewModel>();
            }
            foreach (var product in cookieModel.Products)
            {
                var pr = await this.productService.GetProductAsync(product.ProductId);

                model.Products.Add(new PurchaseProductsAnonymousViewModel
                {

                    Id = pr.Id,
                    PictureUrl = pr.MainPicture.Url,
                    Price = pr.Price,
                    ProductUrl = "/Products/" + pr.Category.Name + "/" + pr.Id,
                    Title = pr.Title,
                    Quantity = product.Quantity
                });
            }

            return this.View(model);
        }


        [AllowAnonymous]
        [HttpGet]
        [ShoppingCartFilter]
        public IActionResult IncreaseProductQuantityAnonymous(string id, int quantity)
        {

            if (this.User.Identity.IsAuthenticated || this.HttpContext.Session.GetString("Cart") == "empty")
            {
                return this.RedirectToAction("Error", "Home");
            }
            else
            {
                var userCookie = JsonConvert.DeserializeObject<CartCookieModel>(this.HttpContext.Session.GetString("Cart"));
                if(userCookie.Products.Sum(x => x.Quantity) == 0)
                {
                    return this.RedirectToAction("Error", "Home");
                }
                else
                {
                    userCookie.Products.FirstOrDefault(x => x.ProductId == id).Quantity = quantity;

                    var jsonModel = JsonConvert.SerializeObject(userCookie);

                    this.HttpContext.Session.SetString("Cart", jsonModel);
                }
            }       

            return this.RedirectToAction("ReviewCartAnonymous");
        }

        [AllowAnonymous]
        [HttpGet]
        [ShoppingCartFilter]
        public IActionResult RemoveProductFromCartAnonymous(string id)
        {

            if (this.User.Identity.IsAuthenticated || this.HttpContext.Session.GetString("Cart") == "empty")
            {
                return this.RedirectToAction("Error", "Home");
            }
            else
            {
                var userCookie = JsonConvert.DeserializeObject<CartCookieModel>(this.HttpContext.Session.GetString("Cart"));
                if (userCookie.Products.Sum(x => x.Quantity) == 0)
                {
                    return this.RedirectToAction("Error", "Home");
                }
                else
                {
                    userCookie.Products.Remove(userCookie.Products.FirstOrDefault(x => x.ProductId == id));

                    var jsonModel = JsonConvert.SerializeObject(userCookie);

                    this.HttpContext.Session.SetString("Cart", jsonModel);
                }
            }


            return this.RedirectToAction("ReviewCartAnonymous");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CheckoutAnonymous(AnonymousCartViewModel form)
        {

            if (this.ModelState.IsValid)
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                if (this.HttpContext.Session.GetString("Cart") == "empty")
                {
                    return this.RedirectToAction("EmptyCart");
                }

                var cookiesJson = this.HttpContext.Session.GetString("Cart");


                var cookieModel = JsonConvert.DeserializeObject<CartCookieModel>(cookiesJson);

                var quantity = cookieModel.Products.Sum(x => x.Quantity);

                if (quantity == 0)
                {
                    return this.RedirectToAction("EmptyCart");
                }

                if(form.Products == null)
                {
                    form.Products = new List<PurchaseProductsAnonymousViewModel>();
                }

                foreach (var product in cookieModel.Products)
                {
                    var pr = await this.productService.GetProductAsync(product.ProductId);

                    form.Products.Add(new PurchaseProductsAnonymousViewModel
                    {

                        Id = pr.Id,
                        PictureUrl = pr.MainPicture.Url,
                        Price = pr.Price,
                        ProductUrl = "/Products/" + pr.Category.Name + "/" + pr.Id,
                        Title = pr.Title,
                        Quantity = product.Quantity
                    });
                }
         
                await this.service.ChechoutAnonymousAsync(form);

                return this.RedirectToAction("CheckoutDetailsAnonymous", form);
            }
            else
            {
                return this.View("ReviewCartAnonymous", form);
            }
              
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CheckoutDetailsAnonymous(AnonymousCartViewModel form)
        {


            var cookiesJson = this.HttpContext.Session.GetString("Cart");


            var cookieModel = JsonConvert.DeserializeObject<CartCookieModel>(cookiesJson);


            if (form.Products == null)
            {
                form.Products = new List<PurchaseProductsAnonymousViewModel>();
            }

            foreach (var product in cookieModel.Products)
            {
                var pr = await this.productService.GetProductAsync(product.ProductId);

                form.Products.Add(new PurchaseProductsAnonymousViewModel
                {

                    Id = pr.Id,
                    PictureUrl = pr.MainPicture.Url,
                    Price = pr.Price,
                    ProductUrl = "/Products/" + pr.Category.Name + "/" + pr.Id,
                    Title = pr.Title,
                    Quantity = product.Quantity
                });
            }

            this.HttpContext.Session.SetString("Cart", "empty");

            return this.View("CheckoutDetails", form);
        }

        public async Task<IActionResult> CheckoutDetails()
        {            
            return this.View("CheckoutDetails", await this.service.GetLastCheckoutDetailsAsync(this.User.Identity.Name));
        }

    }
}