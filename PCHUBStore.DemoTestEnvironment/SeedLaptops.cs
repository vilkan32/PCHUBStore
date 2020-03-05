using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.DemoTestEnvironment.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCHUBStore.DemoTestEnvironment
{
    public class SeedLaptops
    {


        public async Task SeedLaptopAsync(PCHUBDbContext context)
        {
            var basicCharacteristics = File.ReadAllText(@"C:\Users\velis\source\repos\PCHUBStore\PCHUBStore.DemoTestEnvironment\SeedData\LaptopBasicCharacteristics.json");

            var basicChar = JsonConvert.DeserializeObject<List<string>>(basicCharacteristics);

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Name == "Laptops");

            var product = new Product
            {
                Category = category,
                CreatedOn = DateTime.UtcNow,
                ModificationDate = DateTime.UtcNow,

            };

            var databaseBasicCharacteristics = new List<BasicCharacteristic> {

               new BasicCharacteristic
               {
                    Key = "Processor",
                    Value = basicChar[0],
               },

               new BasicCharacteristic
               {
                    Key = "Ram",
                    Value = basicChar[1],
               },

            };

            for (int i = 2; i < basicChar.Count; i++)
            {
                if (basicChar[i].Contains("SSD"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "SSD",
                                Value = basicChar[i],
                            }

                        );

                    continue;
                }

                if (basicChar[i].Contains("HDD"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "HDD",
                                Value = basicChar[i],
                            }

                        );

                    continue;
                }

                if (basicChar[i].Contains("NVIDIA") || basicChar[i].ToLower().Contains("nvidia") || basicChar[i].ToLower().Contains("amd") || basicChar[i].ToLower().Contains("hd graphics"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "VideoCard",
                                Value = basicChar[i],
                            }

                        );

                    continue;
                }


                if (basicChar[i].ToLower().Contains("производител"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "Manufacturer",
                                Value = basicChar[i].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].ToString(),
                            }

                        );

                    product.Make = basicChar[i].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].ToString();
                    continue;
                }


                if (basicChar[i].ToLower().Contains("windows"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "OS",
                                Value = basicChar[i]
                            }

                        );

                    continue;
                }

            }

            foreach (var item in databaseBasicCharacteristics)
            {
                product.BasicCharacteristics.Add(item);
            }

            category.Products.Add(product);

            // <--------------------------> First part with the basic characteristics next is Advanced Characteristics <-------------------------->
            //        Error = (currentObject, errorContext) =>
            //{
            //  errorContext.ErrorContext.Handled = true;
            //},

            var fullCharacteristics = new List<FullCharacteristic>();

            var advancedCharacteristicsJson = File.ReadAllText(@"C:\Users\velis\source\repos\PCHUBStore\PCHUBStore.DemoTestEnvironment\SeedData\LaptopAdvancedCharacteristics.json");

            var advancedChars = JsonConvert.DeserializeObject<List<AdvancedCharacteristicsDTO>>(advancedCharacteristicsJson, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            });



            foreach (var x in advancedChars)
            {
                if (x.Key.ToUpper() == "ДИСПЛЕЙ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Display",
                        Value = x.Value

                    });
                    continue;
                }

                if (x.Key.ToUpper() == "ПРОЦЕСОР")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Processor",
                        Value = x.Value

                    });
                    continue;
                }

                if (x.Key.ToUpper() == "RAM ПАМЕТ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "RAM",
                        Value = x.Value

                    });
                    continue;
                }


                if (x.Key.ToUpper() == "SSD ДИСК")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "SSD",
                        Value = x.Value

                    });
                    continue;
                }


                if (x.Key.ToUpper() == "ВИДЕО КАРТА")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Video Card",
                        Value = x.Value

                    });
                    continue;
                }


                if (x.Key.ToUpper() == "ОПЕРАЦИОННА СИСТЕМА")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "OS",
                        Value = x.Value

                    });
                    continue;
                }

                if (x.Key.ToUpper() == "БЕЗЖИЧНА ВРЪЗКА WI-FI")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "WI-FI Connection",
                        Value = x.Value

                    });
                    continue;
                }


                if (x.Key.ToUpper() == "МРЕЖОВА КАРТА LAN")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "LAN Card",
                        Value = x.Value

                    });
                    continue;
                }

                if (x.Key.ToUpper() == "3G/4G ИНТЕРНЕТ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "3G/4G Internet",
                        Value = x.Value

                    });
                    continue;
                }

                if (x.Key.ToUpper() == "BLUETOOTH")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Bluetooth",
                        Value = x.Value

                    });
                    continue;
                }

                if (x.Key.ToUpper() == "КАМЕРА")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Web Cam",
                        Value = x.Value

                    });
                    continue;
                }

                if (x.Key.ToUpper() == "МИКРОФОН")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Microphone",
                        Value = x.Value

                    });
                    continue;
                }


                if (x.Key.ToUpper() == "АУДИО")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Audio",
                        Value = x.Value

                    });
                    continue;
                }

                if (x.Key.ToUpper() == "СИГУРНОСТ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Security",
                        Value = x.Value

                    });
                    continue;
                }


                if (x.Key.ToUpper() == "ЦВЯТ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Color",
                        Value = x.Value

                    });
                    continue;
                }

                if (x.Key.ToUpper() == "ТЕГЛО")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Weight",
                        Value = x.Value

                    });
                    continue;
                }



                if (x.Key.ToUpper() == "БАТЕРИЯ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Battery",
                        Value = x.Value

                    });
                    continue;
                }


                if (x.Key.ToUpper() == "LED СВЕТЕЩА КЛАВИАТУРА")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Led Keyboard",
                        Value = x.Value

                    });
                    continue;
                }

                if (x.Key.ToUpper() == "MULTI-TOUCH ДИСПЛЕЙ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Multi Touch Display",
                        Value = x.Value

                    });
                    continue;
                }


                if (x.Key.ToUpper() == "СЕРИЯ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Model",
                        Value = x.Value

                    });

                    product.Model = x.Value;
                    continue;
                }


                if (x.Key.ToUpper() == "ГАРАНЦИЯ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Warranty",
                        Value = x.Value

                    });
                    continue;
                }

                if (x.Key == "Price")
                {

                    var price = decimal.Parse(x.Value.Split(" ").ToArray()[0], CultureInfo.InvariantCulture);
                    product.Price = price;

                    continue;
                }

                if (x.Key == "ArticleId")
                {

                    var articleId = x.Value.Split(" ").ToArray()[1].ToString();
                    product.ArticleNumber = articleId;

                    continue;
                }

                if (x.Key == "Title")
                {

                    product.Title = x.Value;
                    continue;
                }

                if (x.Key == "Pictures")
                {
                    var pictures = x.Value.Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray();

                    foreach (var picture in pictures)
                    {
                        product.Pictures.Add(

                            new Picture
                            {
                                CreatedOn = DateTime.UtcNow,
                                IsVideo = false,
                                Name = string.Join("", picture.TakeLast(45).ToArray()),
                                Url = picture,
                                ModificationDate = DateTime.UtcNow,
                            });
                    }

                    product.MainPicture = new Picture
                    {
                        CreatedOn = DateTime.UtcNow,
                        IsVideo = false,
                        Name = string.Join("", pictures[0].TakeLast(45).ToArray()),
                        Url = pictures[0],
                        ModificationDate = DateTime.UtcNow,
                    };

                    continue;
                }

            }

            product.FullCharacteristics = fullCharacteristics;

            await context.SaveChangesAsync();

        }

        public async Task SeedLaptopFiltersAsync(PCHUBDbContext context)
        {

            var laptopCategory = await context.Categories.FirstOrDefaultAsync(x => x.Name == "Laptops");

            var laptops = laptopCategory.Products.ToList();

            // done
            var models = laptops.Select(x => x.Model).ToList();
            // done
            var makes = laptops.Select(x => x.Make).ToList();

            var processors = laptops.Select(x => x.BasicCharacteristics.FirstOrDefault(x => x.Key == "Processor").Value).ToList();

            var videoCardsRaw = laptops.Select(x => (BasicCharacteristic)x.BasicCharacteristics.FirstOrDefault(x => x.Key == "VideoCard")).ToList();

            var videoCardsNoNull = videoCardsRaw.Where(x => x != null);

            var videoCards = videoCardsNoNull.Select(x => x.Value).ToList();

            // Models Filters Start
            var modelsFilterCategory = new FilterCategory
            {
                CategoryName = "Laptops",
                ViewSubCategoryName = "Models",
                CreatedOn = DateTime.UtcNow,
                ModificationDate = DateTime.UtcNow,
            };

            var modelsFilters = new List<PCHUBStore.Data.Models.Filter>();


            foreach (var item in models.Distinct())
            {
                if (item != null)
                {
                    modelsFilters.Add(

                        new PCHUBStore.Data.Models.Filter
                        {
                            Name = "Model",
                            Value = item
                        });
                }
            }

            modelsFilterCategory.Filters = modelsFilters;

 //           await context.FilterCategories.AddAsync(modelsFilterCategory);
            // Models Filters End


            // Makes Filters Start
            var makesFilterCategory = new FilterCategory
            {
                CategoryName = "Laptops",
                ViewSubCategoryName = "Makes",
            };

            var makesFilters = new List<PCHUBStore.Data.Models.Filter>();

            foreach (var item in makes.Distinct())
            {
                if (item != null)
                {
                    makesFilters.Add(

                  new PCHUBStore.Data.Models.Filter
                  {
                      Name = "Make",
                      Value = item
                  });
                }
            }

            makesFilterCategory.Filters = makesFilters;

  //          await context.FilterCategories.AddAsync(makesFilterCategory);
            //     Makes Filters End

            // Processor Filters Start
            var processorsFilterCategory = new FilterCategory
            {
                CategoryName = "Laptops",
                ViewSubCategoryName = "Processors",
            };

            var processorsFilters = new List<PCHUBStore.Data.Models.Filter>
            {

                new PCHUBStore.Data.Models.Filter
                {
                    Name = "Processor",
                    Value = "Intel Core i3"
                },
                new PCHUBStore.Data.Models.Filter
                {
                    Name = "Processor",
                    Value = "Intel Core i5"
                },
                new PCHUBStore.Data.Models.Filter
                {
                    Name = "Processor",
                    Value = "Intel Core i7"
                },
                new PCHUBStore.Data.Models.Filter
                {
                    Name = "Processor",
                    Value = "Intel Pentium"
                },
                new PCHUBStore.Data.Models.Filter
                {
                    Name = "Processor",
                    Value = "Intel Celeron"
                },
                new PCHUBStore.Data.Models.Filter
                {
                    Name = "Processor",
                    Value = "AMD Dual Core"
                },
                new PCHUBStore.Data.Models.Filter
                {
                    Name = "Processor",
                    Value = "AMD Quad Core"
                },
                new PCHUBStore.Data.Models.Filter
                {
                    Name = "Processor",
                    Value = "AMD Ryzen 7"
                },
            };

            processorsFilterCategory.Filters = processorsFilters;
            //     await context.FilterCategories.AddAsync(processorsFilterCategory);
            // end

            var videCardFilterCategory = new FilterCategory
            {
                CategoryName = "Laptops",
                ViewSubCategoryName = "VideoCards",
            };

            var videoCardFilters = new List<PCHUBStore.Data.Models.Filter>();

            foreach (var item in videoCards.Distinct())
            {
                if (item != null)
                {
                    var vc = item.Split(new char[] { ',', '-' }, StringSplitOptions.RemoveEmptyEntries).ToArray()[0];

                    videoCardFilters.Add(
                  new PCHUBStore.Data.Models.Filter
                  {
                      Name = "VideoCard",
                      Value = vc
                  });
                }
            }

            videCardFilterCategory.Filters = videoCardFilters;

            await context.FilterCategories.AddAsync(videCardFilterCategory);
            await context.SaveChangesAsync();
        }

    }


}
