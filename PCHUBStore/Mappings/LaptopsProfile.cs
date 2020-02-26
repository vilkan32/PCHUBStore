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
                .ForMember(x => x.LaptopAdvancedDetailsUrl, y => y.MapFrom(z => "Products/Laptop/" + z.Id));
                


        }
    }
}
