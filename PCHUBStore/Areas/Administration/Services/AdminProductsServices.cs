using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Administration.Models.ProductViewModel;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public class AdminProductsServices : IAdminProductsServices
    {
        private readonly PCHUBDbContext context;
        private readonly ICloudinaryServices cloudinary;

        public AdminProductsServices(PCHUBDbContext context,
            ICloudinaryServices cloudinary)
        {
            this.context = context;
            this.cloudinary = cloudinary;
        }


        public async Task CreateCategoryAsync(string categoryName)
        {
            await this.context.Categories.AddAsync(new Category
            {
                Name = categoryName
            });

            var result = await this.context.SaveChangesAsync();
        }

        public async Task CreateProductAsync(InsertProductViewModel form)
        {
            var product = new Product
            {
                ArticleNumber = form.ArticleNumber,
                Model = form.Model,
                Make = form.Make,
                Quantity = form.Quantity,
                Price = form.Price,
                HtmlDescription = form.HtmlDescription,
                Title = form.Title,

            };

            product.MainPicture = new Picture
            {
                Url = await this.cloudinary.UploadPictureAsync(form.MainPicture, form.MainPicture.Name),
                Name = form.MainPicture.Name
            };

            foreach (var picture in form.Pictures)
            {
                product.Pictures.Add(new Picture
                {

                    Url = await this.cloudinary.UploadPictureAsync(picture, picture.Name),
                    Name = picture.Name
                    
                });
            }


            foreach (var bc in form.BasicCharacteristics.Where(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value)))
            {
                product.BasicCharacteristics.Add(new BasicCharacteristic
                {

                    Key = bc.Key,
                    Value = bc.Value

                });
            }

            foreach (var fc in form.FullCharacteristics.Where(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value)))
            {
                product.FullCharacteristics.Add(new FullCharacteristic
                {

                    Key = fc.Key,
                    Value = fc.Value

                });
            }

            var category = await this.context.Categories.FirstOrDefaultAsync(x => x.Name == form.Category);

            category.Products.Add(product);

            await this.context.SaveChangesAsync();
        }

        public async Task<ICollection<string>> GetAllCategoryNamesAsync()
        {
            return await this.context.Categories.Select(x => x.Name).ToListAsync();
        }
    }
}
