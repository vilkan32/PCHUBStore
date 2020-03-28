using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PCHUBStore.Data;
using PCHUBStore.View.Models.MainSliderApiModels;

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
        public async Task<string> Get()
        {
            var mainSlider = await this.context.MainSliders.FirstOrDefaultAsync(x => x.Name == "MainSlider");

            var mainSliderPictures = mainSlider.MainSliderPictures.Where(x => x.IsDeleted == false);

            var jsonModel = new List<MainSliderPicturesViewModel>();
            
            foreach (var item in mainSliderPictures)
            {
                jsonModel.Add(new MainSliderPicturesViewModel { Url = item.Url, Href = item.RedirectTo });
            }
            var json = JsonConvert.SerializeObject(jsonModel);

            return json;
        }

    }
}
