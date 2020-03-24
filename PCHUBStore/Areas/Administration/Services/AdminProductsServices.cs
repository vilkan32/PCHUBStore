using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PCHUBStore.Areas.Administration.Models.ProductViewModel;
using PCHUBStore.Data;
using PCHUBStore.Data.Models;
using PCHUBStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PCHUBStore.DemoTestEnvironment.DTOs;
using System.Globalization;

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

        public async Task CreateMonitorFromJSONAsync(InsertJsonProductViewModel form)
        {
            var category = await this.context.Categories.FirstOrDefaultAsync(x => x.Name == form.Category);
            var product = new Product();

            var basicCharacteristics = JsonConvert.DeserializeObject<List<string>>(form.BasicCharacteristics);

            var databaseBasicCharacteristics = new List<BasicCharacteristic>();

            var fullCharacteristics = new List<FullCharacteristic>();

            var fullChars = JsonConvert.DeserializeObject<List<AdvancedCharacteristicsDTO>>(form.FullCharacteristics, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            });


            foreach (var bc in basicCharacteristics)
            {
                if (bc.Contains("Размер на дисплея"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Display Size",
                        Value = "Display Size: " + bc.Split(": ")[1]

                    });

                    continue;
                }

                if (bc.Contains("Тип на матрицата"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Matrix Type",
                        Value = "Matrix Type: " + bc.Split(": ")[1]

                    });
                    continue;
                }

                if (bc.Contains("Време за реакция"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Reaction Time",
                        Value = "Reaction Time: " + bc.Split(": ")[1]

                    });

                    continue;
                }

                if (bc.Contains("Резолюция"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Resolution",
                        Value = "Resolution: " + bc.Split(": ")[1]

                    });

                    continue;
                }

                if(bc.Contains("Честота на опресняване"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "FPS",
                        Value = "FPS: " + bc.Split(": ")[1]

                    });

                    continue;
                }

                if (bc.Contains("Яркост"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Brightness",
                        Value = "Brightness: " + bc.Split(": ")[1]

                    });

                    continue;
                }

                if (bc.Contains("Производител"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Make",
                        Value = "Make: " + bc.Split(": ")[1]

                    });

                    product.Make = bc.Split(": ")[1];

                    continue;
                }

                if (bc.Contains("OEM код"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "OEM Code",
                        Value = "OEM Code: " + bc.Split(": ")[1]

                    });

                    continue;
                }
            }

            product.BasicCharacteristics = databaseBasicCharacteristics;

            foreach (var x in fullChars)
            {
                if (x.Key == "РАЗМЕР НА ДИСПЛЕЯ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Display Size",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ТИП НА МАТРИЦАТА")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Matrix Type",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ВРЕМЕ ЗА РЕАКЦИЯ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Reaction Time",
                        Value = x.Value

                    });

                    continue;
                }

                if(x.Key == "ЧЕСТОТА НА ОПРЕСНЯВАНЕ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "FPS",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "РЕЗОЛЮЦИЯ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Resolution",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ФОРМАТ НА КАРТИНАТА")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Picture Format",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ЗРИТЕЛЕН ЪГЪЛ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "View Angle",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ЯРКОСТ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Brightness",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "СТАТИЧЕН КОНТРАСТ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Static Contrast",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ДИНАМИЧЕН КОНТРАСТ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Dynamic Contrast",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ПОДСВЕТКА")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Back Light",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ПОРТОВЕ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Ports",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ТЕХНОЛОГИИ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Technology",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ДРУГО")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Other",
                        Value = x.Value

                    });
                    continue;
                }

                if (x.Key == "HDR")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "HDR",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ГАРАНЦИЯ")
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

            category.Products.Add(product);

            await this.context.SaveChangesAsync();
        }

        public async Task CreateLaptopFromJSONAsync(InsertJsonProductViewModel form)
        {
            var category = await this.context.Categories.FirstOrDefaultAsync(x => x.Name == form.Category);

            var product = new Product();

            var basicCharacteristics = JsonConvert.DeserializeObject<List<string>>(form.BasicCharacteristics);

            var fullCharacteristics = new List<FullCharacteristic>();

            var advancedChars = JsonConvert.DeserializeObject<List<AdvancedCharacteristicsDTO>>(form.FullCharacteristics, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            });


            var databaseBasicCharacteristics = new List<BasicCharacteristic> {

               new BasicCharacteristic
               {
                    Key = "Processor",
                    Value = basicCharacteristics[0],
               },

               new BasicCharacteristic
               {
                    Key = "Ram",
                    Value = basicCharacteristics[1],
               },

            };

            for (int i = 2; i < basicCharacteristics.Count; i++)
            {
                if (basicCharacteristics[i].Contains("SSD"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "SSD",
                                Value = basicCharacteristics[i],
                            }

                        );

                    continue;
                }

                if (basicCharacteristics[i].Contains("HDD"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "HDD",
                                Value = basicCharacteristics[i],
                            }

                        );

                    continue;
                }

                if (basicCharacteristics[i].Contains("NVIDIA") || basicCharacteristics[i].ToLower().Contains("nvidia") || basicCharacteristics[i].ToLower().Contains("amd") || basicCharacteristics[i].ToLower().Contains("hd graphics"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "VideoCard",
                                Value = basicCharacteristics[i],
                            }

                        );

                    continue;
                }


                if (basicCharacteristics[i].ToLower().Contains("производител"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "Make",
                                Value = basicCharacteristics[i].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].ToString(),
                            }

                        );

                    product.Make = basicCharacteristics[i].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].ToString();
                    continue;
                }


                if (basicCharacteristics[i].ToLower().Contains("windows"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "OS",
                                Value = basicCharacteristics[i].Split(",")[0]
                            }

                        );

                    continue;
                }

            }

            product.BasicCharacteristics = databaseBasicCharacteristics;


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
                        Value = x.Value.Split(",")[0].Split(": ")[0]

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

            category.Products.Add(product);

            await this.context.SaveChangesAsync();

        }

        public async Task CreateComputerFromJSONAsync(InsertJsonProductViewModel form)
        {
            var category = await this.context.Categories.FirstOrDefaultAsync(x => x.Name == form.Category);

            var product = new Product();

            var basicCharacteristics = JsonConvert.DeserializeObject<List<string>>(form.BasicCharacteristics);

            var fullCharacteristics = new List<FullCharacteristic>();

            var advancedChars = JsonConvert.DeserializeObject<List<AdvancedCharacteristicsDTO>>(form.FullCharacteristics, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            });


            var databaseBasicCharacteristics = new List<BasicCharacteristic> {

               new BasicCharacteristic
               {
                    Key = "Processor",
                    Value = basicCharacteristics[0],
               },

               new BasicCharacteristic
               {
                    Key = "Ram",
                    Value = basicCharacteristics[1],
               },

            };

            for (int i = 2; i < basicCharacteristics.Count; i++)
            {
                if (basicCharacteristics[i].Contains("SSD"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "SSD",
                                Value = basicCharacteristics[i].Split(": ")[0],
                            }

                        );

                    continue;
                }

                if (basicCharacteristics[i].Contains("HDD") || basicCharacteristics[i].Contains("SATA"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "HDD",
                                Value = basicCharacteristics[i].Split(": ")[0],
                            }

                        );

                    continue;
                }

                if (basicCharacteristics[i].Contains("NVIDIA") || basicCharacteristics[i].ToLower().Contains("nvidia") || basicCharacteristics[i].ToLower().Contains("amd") || basicCharacteristics[i].ToLower().Contains("Radeon") || basicCharacteristics[i].ToLower().Contains("hd graphics"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "VideoCard",
                                Value = basicCharacteristics[i],
                            }

                        );

                    continue;
                }


                if (basicCharacteristics[i].ToLower().Contains("производител"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "Make",
                                Value = basicCharacteristics[i].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].ToString(),
                            }

                        );

                    product.Make = basicCharacteristics[i].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].ToString();
                    continue;
                }


                if (basicCharacteristics[i].ToLower().Contains("windows"))
                {
                    databaseBasicCharacteristics.Add(
                            new BasicCharacteristic
                            {
                                Key = "OS",
                                Value = basicCharacteristics[i].Split(",")[0]
                            }

                        );

                    continue;
                }

            }

            product.BasicCharacteristics = databaseBasicCharacteristics;


            foreach (var x in advancedChars)
            {
 

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
                        Value = x.Value.Split(",")[0].Split(": ")[0]

                    });
                    continue;
                }

                if (x.Key.ToUpper() == "ХАРД ДИСК")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {

                        Key = "Hard Disc",
                        Value = x.Value.Split(",")[0].Split(": ")[0]

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
                        Value = x.Value.Split(",")[0].Split(": ")[0]

                    });
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

            category.Products.Add(product);

            await this.context.SaveChangesAsync();

        }

        public async Task CreateKeyboardFromJSONAsync(InsertJsonProductViewModel form)
        {
            var category = await this.context.Categories.FirstOrDefaultAsync(x => x.Name == form.Category);
            var product = new Product();

            var basicCharacteristics = JsonConvert.DeserializeObject<List<string>>(form.BasicCharacteristics);

            var databaseBasicCharacteristics = new List<BasicCharacteristic>();

            var fullCharacteristics = new List<FullCharacteristic>();

            var fullChars = JsonConvert.DeserializeObject<List<AdvancedCharacteristicsDTO>>(form.FullCharacteristics, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            });


            foreach (var bc in basicCharacteristics)
            {
                if (bc.Contains("Тип"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Type",
                        Value = "Type: " + bc.Split(": ")[1]

                    });

                    continue;
                }

                if (bc.Contains("Интерфейс"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Interface",
                        Value = "Interface: " + bc.Split(": ")[1]

                    });

                    continue;
                }

                if (bc.Contains("Мултимедийни бутони"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Multimedia Buttons",
                        Value = "Multimedia Buttons: " + bc.Split(": ")[1]

                    });

                    continue;
                }

                if (bc.Contains("LED подсветка"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "LED Light",
                        Value = "LED Light: " + bc.Split(": ")[1]

                    });

                    continue;
                }

                if (bc.Contains("Механична"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Mechanical",
                        Value = "Mechanical: " + bc.Split(": ")[1]

                    });

                    continue;
                }


                if (bc.Contains("Производител"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Make",
                        Value = "Make: " + bc.Split(": ")[1]

                    });

                    product.Make = bc.Split(": ")[1];
                    continue;
                }

            }

            product.BasicCharacteristics = databaseBasicCharacteristics;

            foreach (var x in fullChars)
            {
             
                if(x.Key == "ТИП")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Type",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ИНТЕРФЕЙС")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Interface",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ОПИСАНИЕ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Description",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ЦВЯТ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Color",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "МУЛТИМЕДИЙНИ БУТОНИ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Multimedia Buttons",
                        Value = x.Value

                    });

                    continue;
                }


                if (x.Key == "LED ПОДСВЕТКА")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "LED Light",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "МЕХАНИЧНА КЛАВИАТУРА")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Mechanical Keyboard",
                        Value = x.Value

                    });

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

            category.Products.Add(product);
            await this.context.SaveChangesAsync();

        }

        public async Task CreateMouseFromJSONAsync(InsertJsonProductViewModel form)
        {
            var category = await this.context.Categories.FirstOrDefaultAsync(x => x.Name == form.Category);
            var product = new Product();

            var basicCharacteristics = JsonConvert.DeserializeObject<List<string>>(form.BasicCharacteristics);

            var databaseBasicCharacteristics = new List<BasicCharacteristic>();

            var fullCharacteristics = new List<FullCharacteristic>();

            var fullChars = JsonConvert.DeserializeObject<List<AdvancedCharacteristicsDTO>>(form.FullCharacteristics, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            });

            foreach (var bc in basicCharacteristics)
            {
                if (bc.Contains("Свързване"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Connectivity",
                        Value = "Connectivity: " + bc.Split(": ")[1]

                    });

                    continue;
                }

                if (bc.Contains("Сензор"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Sensor",
                        Value = "Sensor: " + bc.Split(": ")[1]

                    });

                    continue;
                }

                if (bc.Contains("Екстри")) 
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Features",
                        Value = "Features: " + bc.Split(": ")[1]

                    });

                    continue;

                }

                if (bc.Contains("Интерфейс"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Interface",
                        Value = "Interface: " + bc.Split(": ")[1]

                    });

                    continue;
                }

                if (bc.Contains("Разделителна способност"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "DPI",
                        Value = "DPI: " + bc.Split(": ")[1]

                    });

                    continue;
                }

                if (bc.Contains("Брой бутони"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Buttons",
                        Value = "Buttons: " + bc.Split(": ")[1]

                    });

                    continue;
                }


                if (bc.Contains("Производител"))
                {
                    databaseBasicCharacteristics.Add(new BasicCharacteristic
                    {

                        Key = "Make",
                        Value = "Make: " + bc.Split(": ")[1]

                    });

                    product.Make = bc.Split(": ")[1];
                    continue;
                }

            }

            product.BasicCharacteristics = databaseBasicCharacteristics;

            foreach (var x in fullChars)
            {

                if (x.Key == "СВЪРЗВАНЕ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Connectivity",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "СПЕЦИАЛИЗИРАНА ЗА ИГРИ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Gaming",
                        Value = x.Value

                    });

                    continue;
                }


                if (x.Key == "ОПИСАНИЕ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Description",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ТЪЧ ФУНКЦИЯ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Touch Function",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "СЕНЗОР")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Sensor",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ИНТЕРФЕЙС")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Interface",
                        Value = x.Value

                    });

                    continue;
                }


                if (x.Key == "РАЗДЕЛИТЕЛНА СПОСОБНОСТ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "DPI",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "БРОЙ БУТОНИ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Buttons",
                        Value = x.Value

                    });

                    continue;
                }

                if (x.Key == "ЦВЯТ")
                {
                    fullCharacteristics.Add(new FullCharacteristic
                    {
                        Key = "Color",
                        Value = x.Value

                    });

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

            category.Products.Add(product);
            await this.context.SaveChangesAsync();
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
