using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PCHUBStore.Data;
using PCHUBStore.View.Models.ApiViewModels;

namespace PCHUBStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewedProductsController : ControllerBase
    {
        private readonly PCHUBDbContext context;
        private readonly IMapper mapper;

        public ReviewedProductsController(PCHUBDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/ReviewedProducts
        [HttpGet]
        public async Task<string> Get()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == this.User.Identity.Name);

                var history = this.mapper.Map<List<ApiProductHistoryViewModel>>(user.ProductUserReviews.Select(x => x.Product).TakeLast(10));

                var json = JsonConvert.SerializeObject(history);

                return json;
            }

            return "";

        }


    }
}
