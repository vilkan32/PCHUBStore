using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PCHUBStore.MiddlewareFilters;
using PCHUBStore.Models;
using PCHUBStore.Services;

namespace PCHUBStore.Controllers
{
    [Authorize(Roles = "StoreUser")]
    public class ShoppingCartController : Controller
    {
        private readonly IShopServices service;

        public ShoppingCartController(IShopServices service)
        {
            this.service = service;
        }
        [HttpGet]
        public IActionResult ReviewCart()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult ExcludeProduct()
        {
            return this.View();
        }

        [AllowAnonymous]
        [HttpGet("/Cart")]
        [ShoppingCartFilter]
        public IActionResult ReviewCartAnonymous()
        {
            return this.View();
        }


        [HttpGet]
        public async Task<IActionResult> BuyProduct([Required][MaxLength(80)]string id)
        {
            await this.service.BuyProductAsync(this.User.Identity.Name, id);


            return this.RedirectToAction("ReviewCart");
        }

        [AllowAnonymous]
        [ShoppingCartFilter]
        [HttpGet]
        public IActionResult BuyProductAnonymous([Required][MaxLength(80)]string id)
        {
            if (this.HttpContext.Session.GetString("Cart") == "empty")
            {

                var model = new CartCookieModel();

                model.ProductIds.Add(id);

                var jsonModel = JsonConvert.SerializeObject(model);

                this.HttpContext.Session.SetString("Cart", jsonModel);
            }
            else
            {
                var model = JsonConvert.DeserializeObject<CartCookieModel>(this.HttpContext.Session.GetString("Cart"));
                model.ProductIds.Add(id);

                var jsonModel = JsonConvert.SerializeObject(model);

                this.HttpContext.Session.SetString("Cart", jsonModel);
            }

            return StatusCode(201);
        }
    }
}