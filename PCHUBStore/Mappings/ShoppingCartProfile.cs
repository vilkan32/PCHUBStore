using AutoMapper;
using PCHUBStore.Data.Models;
using PCHUBStore.Models;
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


            CreateMap<Product, ProductCookieModel>()
            .ForMember(x => x.Quantity, y => y.Ignore())
            .ForMember(x => x.ProductId, y => y.MapFrom(z => z.Id));



            CreateMap<CartCookieModel, AnonymousCartViewModel>()
            .ForMember(x => x.Address, y => y.MapFrom(z => z.Address))
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName))
            .ForMember(x => x.City, y => y.MapFrom(z => z.City))
            .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
            .ForMember(x => x.Products, y => y.Ignore())
            .ForMember(x => x.PhoneNumber, y => y.MapFrom(z => z.PhoneNumber));

        }
    }
}
