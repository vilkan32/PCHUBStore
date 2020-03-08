using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PCHUBStore.Data.Models;
using PCHUBStore.View.Models;

namespace PCHUBStore.Mappings
{
    public class LaptopsProfile : Profile
    {
        public LaptopsProfile()
        {
            //          from          to
            CreateMap<Product, LaptopViewModel>()
                .ForMember(x => x.BasicCharacteristics, y => y.MapFrom(z => z.BasicCharacteristics.Select(x => x.Value)))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                .ForMember(x => x.PictureUrl, y => y.MapFrom(z => z.MainPicture.Url))
                .ForMember(x => x.Title, y => y.MapFrom(z => new string(z.Title.Take(100).ToArray())))
                .ForMember(x => x.LaptopAdvancedDetailsUrl, y => y.MapFrom(z => "Products/Laptop/" + z.Id));


            //          from          to
            CreateMap<FullCharacteristic, LaptopAdvancedDetailsViewModel>()
                 .ForMember(x => x.Key, y => y.MapFrom(z => z.Key + " :"))
                 .ForMember(x => x.Value, y => y.MapFrom(z => z.Value));

            //          from          to
            CreateMap<Product, LaptopFullCharacteristicsViewModel>()
                 .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                 .ForMember(x => x.ArticleNumber, y => y.MapFrom(z => z.ArticleNumber))
                 .ForMember(x => x.Make, y => y.MapFrom(z => z.Make))
                 .ForMember(x => x.Model, y => y.MapFrom(z => z.Model))
                 .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                 .ForMember(x => x.Title, y => y.MapFrom(z => z.Title))
                 .ForMember(x => x.MainPicture, y => y.MapFrom(z => z.MainPicture.Url))
                 .ForMember(x => x.Pictures, y => y.MapFrom(z => z.Pictures.Select(x => x.Url)))
                 .ForMember(x => x.AdvancedDetails, y => y.MapFrom(z => z.FullCharacteristics))
                 .ForMember(x => x.BasicDetails, y => y.MapFrom(z => z.BasicCharacteristics.Select(x => x.Value)));

          
        }
    }
}
