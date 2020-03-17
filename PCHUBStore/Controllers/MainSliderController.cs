using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PCHUBStore.Data;

namespace PCHUBStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainSliderController : ControllerBase
    {
        private readonly PCHUBDbContext context;

        public MainSliderController(PCHUBDbContext context)
        {
            this.context = context;
        }
        // GET: api/MainSlider
        [HttpGet]
        public string Get()
        {
            var pictures = this.context.Pictures.Where(x => x.Name.Contains("MainSlider")).Select(x => x.Url).ToList();

            var json = JsonConvert.SerializeObject(pictures);

            return json;
        }

    }
}
