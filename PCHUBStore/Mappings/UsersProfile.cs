using AutoMapper;
using PCHUBStore.Data.Models;
using PCHUBStore.View.Models.UserProfileViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Mappings
{
    public class UsersProfile : Profile
    {

        public UsersProfile()
        {
            // UserProfileViewModel

            //          from          to
            CreateMap<User, UserProfileViewModel>()
                .ForMember(x => x.Address, y => y.MapFrom(z => z.Address))
                .ForMember(x => x.Email, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.FirstName, y => y.MapFrom(z => z.FirstName))
                .ForMember(x => x.LastName, y => y.MapFrom(z => z.LastName))
                .ForMember(x => x.Phone, y => y.MapFrom(z => z.PhoneNumber))
                .ForMember(x => x.Username, y => y.MapFrom(z => z.UserName))
                .ForMember(x => x.ProfilePictureUrl, y => y.MapFrom(z => z.ProfilePicture.Url));

            // ProductHistoryViewModel

            //          from          to
            CreateMap<Product, ProductHistoryViewModel>()
                    .ForMember(x => x.Title, y => y.MapFrom(z => string.Join("", z.Title.Take(60).ToList()) + "..."))
                    .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                    .ForMember(x => x.LinkToProduct, y => y.MapFrom(z => "/Products/" + z.Category.Name + "/" + z.Id ))
                    .ForMember(x => x.PictureUrl, y => y.MapFrom(z => z.MainPicture.Url));

            // ProductFavoriteViewModel
            //          from          to
            CreateMap<Product, ProductFavoriteViewModel>()
                   .ForMember(x => x.Title, y => y.MapFrom(z => string.Join("", z.Title.Take(60).ToList()) + "..."))
                   .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                   .ForMember(x => x.LinkToProduct, y => y.MapFrom(z => "/Products/" + z.Category.Name + "/" + z.Id))
                   .ForMember(x => x.PictureUrl, y => y.MapFrom(z => z.MainPicture.Url));

            // ShipmentProduct to UserOrdersViewModel

            CreateMap<ShipmentProduct, UserOrdersViewModel>()
                   .ForMember(x => x.ShipmentId, y => y.MapFrom(z => z.ShipmentId))
                    .ForMember(x => x.ProductId, y => y.MapFrom(z => z.ProductId))
                    .ForMember(x => x.Category, y => y.MapFrom(z => z.Product.Category.Name))
                    .ForMember(x => x.Make, y => y.MapFrom(z => z.Product.Make))
                     .ForMember(x => x.Model, y => y.MapFrom(z => z.Product.Model))
                      .ForMember(x => x.Price, y => y.MapFrom(z => z.Product.Price * z.Quantity))
                       .ForMember(x => x.PictureUrl, y => y.MapFrom(z => z.Product.MainPicture.Url))
                        .ForMember(x => x.Title, y => y.MapFrom(z => z.Product.Title))
                       .ForMember(x => x.PurchaseDate, y => y.MapFrom(z => z.Shipment.PurchaseDate));

        }
    }
}
