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
    public class HomeService : IHomeService
    {
        private readonly PCHUBDbContext context;   

        public HomeService(PCHUBDbContext context)
        {
            this.context = context;

        }
        public async Task<IEnumerable<Product>> GetUserReviewedProductsAsync(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            var userReviews = user.ProductUserReviews.Select(x => x.Product).TakeLast(10);

            return userReviews;

        }

        public async Task<List<Picture>> GetMainSliderPicturesAsync()
        {
            var mainSlider = await this.context.MainSliders.FirstOrDefaultAsync(x => x.Name == "MainSlider");

            var pictures = mainSlider.MainSliderPictures.Where(x => x.IsDeleted == false).ToList();

            return pictures;
        }

        

        public async Task<Page> LoadIndexPageComponentsAsync()
        {
            var indexModel = await this.context.Pages.FirstOrDefaultAsync(x => x.IsDeleted == false && x.PageName == "Index");

            return indexModel;
        }
    }
}
