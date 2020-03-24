using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PCHUBStore.Data;

namespace PCHUBStore.Areas.Administration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplyCharacteristicsController : ControllerBase
    {
        private readonly PCHUBDbContext context;

        public ApplyCharacteristicsController(PCHUBDbContext context)
        {
            this.context = context;
        }
        // GET: api/ApplyCharacteristics fetch('/api/ApplyCharacteristics?category=Laptops').then(x => x.json()).then(x => console.log(x.fullChar));
        [HttpGet]
        public async Task<string> Get(string category)
        {
   
            var cat = await this.context.AdminCharacteristicsCategories.FirstOrDefaultAsync(x => x.CategoryName == category);

            var basicChar = cat.BasicCharacteristics.Select(x => x.Name);
            var fullChar = cat.FullCharacteristics.Select(x => x.Name);

            var param = new { basicChar, fullChar };
            var json = JsonConvert.SerializeObject(param, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return json;
        }
    }
}