using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PCHUBStore.Data.Models;
using PCHUBStore.View.Models;

namespace PCHUBStore.Mappings
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            //          from          to
            CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.BasicCharacteristics, y => y.MapFrom(z => z.BasicCharacteristics.Select(x => x.Value)))
                .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                .ForMember(x => x.PictureUrl, y => y.MapFrom(z => z.MainPicture.Url))
                .ForMember(x => x.Title, y => y.MapFrom(z => new string(z.Title.Take(100).ToArray())))
                .ForMember(x => x.ProductAdvancedDetailsUrl, y => y.MapFrom(z => $"/Products/{z.Category.Name}/" + z.Id));


            //          from          to
            CreateMap<FullCharacteristic, ProductAdvancedDetailsViewModel>()
                 .ForMember(x => x.Key, y => y.MapFrom(z => z.Key + " :"))
                 .ForMember(x => x.Value, y => y.MapFrom(z => z.Value));

            //          from          to
            CreateMap<Product, ProductFullCharacteristicsViewModel>()
                 .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                 .ForMember(x => x.ArticleNumber, y => y.MapFrom(z => z.ArticleNumber))
                 .ForMember(x => x.Make, y => y.MapFrom(z => z.Make))
                 .ForMember(x => x.Model, y => y.MapFrom(z => z.Model))
                 .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                 .ForMember(x => x.Title, y => y.MapFrom(z => z.Title))
                 .ForMember(x => x.MainPicture, y => y.MapFrom(z => z.MainPicture.Url))
                 .ForMember(x => x.Pictures, y => y.MapFrom(z => z.Pictures.Select(x => x.Url)))
                 .ForMember(x => x.AdvancedDetails, y => y.MapFrom(z => z.FullCharacteristics))
                 .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                 .ForMember(x => x.Category, y => y.MapFrom(z => z.Category.Name))
                 .ForMember(x => x.BasicDetails, y => y.MapFrom(z => z.BasicCharacteristics.Select(x => x.Value)));

            //          from          to
            CreateMap<Product, SimilarProduct>()
                 .ForMember(x => x.Price, y => y.MapFrom(z => z.Price.ToString()))
                 .ForMember(x => x.Url, y => y.MapFrom(z => z.Id))
                 .ForMember(x => x.MainPicture, y => y.MapFrom(z => z.MainPicture.Url))
                 .ForMember(x => x.Model, y => y.MapFrom(z => z.Model))
                 .ForMember(x => x.Make, y => y.MapFrom(z => z.Make));
        }
    }
}
