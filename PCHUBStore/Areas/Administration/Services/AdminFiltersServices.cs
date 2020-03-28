using Microsoft.EntityFrameworkCore;
using PCHUBStore.Areas.Administration.Models.FilterViewModels;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Areas.Administration.Services
{
    public class AdminFiltersServices : IAdminFiltersServices
    {
        private readonly PCHUBDbContext context;

        public AdminFiltersServices(PCHUBDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> BasicFiltersExistForCategoryAsync(string category)
        {
            return await this.context.FilterCategories.AnyAsync(x => x.CategoryName == category && (x.ViewSubCategoryName == "Order by" || x.ViewSubCategoryName == "Price"));
        }

        public async Task CreateBasicFiltersAsync(InsertBasicFiltersViewModel form)
        {
            await this.context.FilterCategories.AddAsync(new FilterCategory
            {
                CategoryName = form.Category,
                ViewSubCategoryName = "Order By",
                Filters = new List<PCHUBStore.Data.Models.Filter>
                {
                    new PCHUBStore.Data.Models.Filter
                    {
                        Name = "Price Ascending",
                        Value = "PriceAsc"
                    },
                    new PCHUBStore.Data.Models.Filter
                    {
                        Name = "Price Descending",
                        Value = "PriceDesc"
                    },
                    new PCHUBStore.Data.Models.Filter
                    {
                        Name = "Default",
                        Value = "Default",

                    },
                }
            });

            await this.context.FilterCategories.AddAsync(new FilterCategory
            {
                CategoryName = form.Category,
                ViewSubCategoryName = "Price",
                Filters = new List<PCHUBStore.Data.Models.Filter>
                {
                    new PCHUBStore.Data.Models.Filter
                    {
                        Name = "MinPrice",
                        Value = "0"
                    },
                    new PCHUBStore.Data.Models.Filter
                    {
                        Name = "MaxPrice",
                        Value = "9999"
                    },
                }
            });

            await this.context.SaveChangesAsync();
        }

        public async Task CreateFilterCategoryAsync(InserFilterCategoryViewModel form)
        {
            await this.context.FilterCategories.AddAsync(new FilterCategory
            {

                CategoryName = form.Category,
                ViewSubCategoryName = form.CategoryViewSubName,

            });

            await this.context.SaveChangesAsync();
        }

        public Task<bool> FilterForCategoryExistsAsync(string category, string viewSub)
        {
            return this.context.FilterCategories.AnyAsync(x => x.CategoryName == category && x.ViewSubCategoryName == viewSub);
        }

        public async Task UpdateCategoryAsync(string category)
        {

            if (category == "Laptops")
            {

                var filterCategory = await this.context.FilterCategories.Where(x => x.CategoryName == "Laptops").ToListAsync();

                var productsCategory = await this.context.Categories.FirstOrDefaultAsync(x => x.Name == category);

                var bc = productsCategory.Products.Where(x => x.IsDeleted == false).Where(x => x.Model != null).SelectMany(x => x.BasicCharacteristics);

                var fc = productsCategory.Products.Where(x => x.IsDeleted == false).Where(x => x.Model != null).SelectMany(x => x.FullCharacteristics);



                var models = productsCategory.Products.Select(x => x.Model).Where(x => x != null).Distinct().ToList();

                models.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Model").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Model"

                    });


                });


                var makes = productsCategory.Products.Select(x => x.Make).Where(x => x != null).Distinct().ToList();

                makes.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Make").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Make"

                    });


                });

                var videoCards = bc.Where(x => x.Key == "VideoCard").Select(x => x.Value).Distinct().ToList();

                videoCards.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Video Card").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "VideoCard"

                    });


                });

                var processors = new List<string>
            {
                "Intel Core i5",
                "Intel Core i3",
                "Intel Core i7",
                "Intel Core i9",
                "AMD Ryzen 5",
                "AMD Ryzen 7 ",
                "AMD",
                "Intel Pentium"
            };

                processors.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Processor").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Processor"

                    });


                });

                var ram = new List<string>
            {
                "8 GB",
                "4 GB",
                "16 GB",
                "32 GB",
                "12 GB"
            };

                ram.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "RAM").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "RAM"

                    });


                });


                var os = fc.Where(x => x.Key == "OS").Select(x => x.Value).Distinct().ToList();

                os.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "OS").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "OS"

                    });


                });

                await this.context.SaveChangesAsync();
            }
            else if (category == "Computers")
            {
                var filterCategory = await this.context.FilterCategories.Where(x => x.CategoryName == "Computers").ToListAsync();

                var productsCategory = await this.context.Categories.FirstOrDefaultAsync(x => x.Name == category);

                var bc = productsCategory.Products.Where(x => x.IsDeleted == false).SelectMany(x => x.BasicCharacteristics);

                var fc = productsCategory.Products.Where(x => x.IsDeleted == false).SelectMany(x => x.FullCharacteristics);

                var videoCards = bc.Where(x => x.Key == "VideoCard").Select(x => x.Value).Distinct().ToList();

                videoCards.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Video Card").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "VideoCard"

                    });


                });

                var processors = new List<string>
            {
                "Intel Core i5",
                "Intel Core i3",
                "Intel Core i7",
                "Intel Core i9",
                "AMD Ryzen 3",
                "AMD Ryzen 5",
                "AMD Ryzen 7 ",
                "AMD",
                "Intel Pentium"
            };

                processors.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Processor").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Processor"

                    });


                });

                var ram = new List<string>
            {
                "8 GB",
                "64 GB",
                "16 GB",
                "32 GB",
            };

                ram.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "RAM").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "RAM"

                    });


                });


                var os = fc.Where(x => x.Key == "OS").Select(x => x.Value).Distinct().ToList();

                os.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "OS").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "OS"

                    });


                });

                await this.context.SaveChangesAsync();
            }
            else if(category == "Monitors")
            {

                var filterCategory = await this.context.FilterCategories.Where(x => x.CategoryName == "Monitors").ToListAsync();

                var productsCategory = await this.context.Categories.FirstOrDefaultAsync(x => x.Name == category);

                var bc = productsCategory.Products.Where(x => x.IsDeleted == false).SelectMany(x => x.BasicCharacteristics);

                var fc = productsCategory.Products.Where(x => x.IsDeleted == false).SelectMany(x => x.FullCharacteristics);


                var makes = productsCategory.Products.Select(x => x.Make).Where(x => x != null).Distinct().ToList();

                makes.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Make").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Make"

                    });


                });

                var resolutions = bc.Where(x => x.Key == "Resolution").Select(x => x.Value).Select(x => x.Split(": ")[1]).Distinct().ToList();



                resolutions.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Resolution").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Resolution"

                    });
                });

                var FPSs = bc.Where(x => x.Key == "FPS").Select(x => x.Value).Select(x => x.Split(": ")[1].ToString()).Distinct().ToList();

                FPSs.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "FPS").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "FPS"

                    });
                });

                var reactionTimes = bc.Where(x => x.Key == "Reaction Time").Select(x => x.Value).Select(x => x.Split(": ")[1].ToString()).Distinct().ToList();

                reactionTimes.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Reaction Time").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "ReactionTime"

                    });
                });

                var matrixTypes = bc.Where(x => x.Key == "Matrix Type").Select(x => x.Value).Select(x => x.Split(": ")[1].ToString()).Distinct().ToList();

                matrixTypes.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Matrix Type").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "MatrixType"

                    });
                });


                var displaySizes = bc.Where(x => x.Key == "Display Size").Select(x => x.Value).Select(x => x.Split(": ")[1].ToString()).Distinct().ToList();

                displaySizes.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Display Size").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "DisplaySize"

                    });
                });

                await this.context.SaveChangesAsync();

            }
            else if(category == "Mice")
            {
                var filterCategory = await this.context.FilterCategories.Where(x => x.CategoryName == "Mice").ToListAsync();

                var productsCategory = await this.context.Categories.FirstOrDefaultAsync(x => x.Name == category);

                var bc = productsCategory.Products.Where(x => x.IsDeleted == false).SelectMany(x => x.BasicCharacteristics);

                var fc = productsCategory.Products.Where(x => x.IsDeleted == false).SelectMany(x => x.FullCharacteristics);


                var makes = productsCategory.Products.Select(x => x.Make).Where(x => x != null).Distinct().ToList();

                makes.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Make").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Make"

                    });


                });

                var gaming = fc.Where(x => x.Key == "Gaming").Select(x => x.Value).Distinct().ToList();
                gaming.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Gaming").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Gaming"

                    });
                });

                var connectiviry = bc.Where(x => x.Key == "Connectivity").Select(x => x.Value).Select(x => x.Split(": ")[1]).Distinct().ToList();
                connectiviry.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Connectivity").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Connectivity"

                    });
                });

                var interfaces = bc.Where(x => x.Key == "Interface").Select(x => x.Value).Select(x => x.Split(": ")[1]).Distinct().ToList();
                interfaces.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Interface").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Interface"

                    });
                });

                await this.context.SaveChangesAsync();
            }
            else if(category == "Keyboards")
            {
                var filterCategory = await this.context.FilterCategories.Where(x => x.CategoryName == "Keyboards").ToListAsync();

                var productsCategory = await this.context.Categories.FirstOrDefaultAsync(x => x.Name == category);

                var bc = productsCategory.Products.Where(x => x.IsDeleted == false).SelectMany(x => x.BasicCharacteristics);

                var fc = productsCategory.Products.Where(x => x.IsDeleted == false).SelectMany(x => x.FullCharacteristics);


                var makes = productsCategory.Products.Select(x => x.Make).Where(x => x != null).Distinct().ToList();

                makes.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Make").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Make"

                    });
                });

                var types = bc.Where(x => x.Key == "Type").Select(x => x.Value).Select(x => x.Split(": ")[1]).Distinct().ToList();

                types.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Type").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Type"

                    });
                });

                var interfaces = bc.Where(x => x.Key == "Interface").Select(x => x.Value).Select(x => x.Split(": ")[1]).Distinct().ToList();

                interfaces.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Interface").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Interface"

                    });
                });

                var mechanicals = bc.Where(x => x.Key == "Mechanical").Select(x => x.Value).Select(x => x.Split(": ")[1]).Distinct().ToList();
                mechanicals.ForEach(x =>
                {

                    filterCategory.FirstOrDefault(x => x.ViewSubCategoryName == "Mechanical").Filters.Add(new Data.Models.Filter
                    {

                        Value = x,
                        Name = "Mechanical"

                    });
                });

                await this.context.SaveChangesAsync();
            }
        }
    }
}
