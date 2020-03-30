using AutoMapper;
using PCHUBStore.Data.Models;
using PCHUBStore.View.Models.ShoppingCartViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Mappings
{
    public class ShoppingCartProfile : Profile
    {

        public ShoppingCartProfile()
        {
            CreateMap<ProductCart, ReviewCartViewModel>()
               .ForMember(x => x.Quantity, y => y.MapFrom(z => z.Quantity))
               .ForMember(x => x.Title, y => y.MapFrom(z => z.Product.Title))
               .ForMember(x => x.Price, y => y.MapFrom(z => z.Product.Price))
               .ForMember(x => x.RemoveFromCart, y => y.Ignore())
               .ForMember(x => x.Id, y => y.MapFrom(z => z.ProductId))
                .ForMember(x => x.ProductUrl, y => y.MapFrom(z => "/Products/" + z.Product.Category.Name + "/" + z.ProductId))
               .ForMember(x => x.PictureUrl, y => y.MapFrom(z => z.Product.MainPicture.Url));
        }
    }
}
