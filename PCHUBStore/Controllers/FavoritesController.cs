using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;

namespace PCHUBStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly PCHUBDbContext context;

        public FavoritesController(PCHUBDbContext context)
        {
            this.context = context;
        }
        // GET: api/Favorites
        [HttpGet]
        public async Task<bool> Get(string id)
        {
            var result = false;

            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == this.User.Identity.Name);

            var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            if (!user.FavoriteUserProducts.Select(x => x.Product).Any(x => x.Id == id))
            {
                user.FavoriteUserProducts.Add(new ProductUserFavorite
                {
                    User = user,
                    Product = product
                });                
                await this.context.SaveChangesAsync();
                result = true;
            }

            return result;

        }
       
    }
}
