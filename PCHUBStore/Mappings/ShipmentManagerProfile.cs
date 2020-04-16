using AutoMapper;
using PCHUBStore.Areas.Support.Models;
using PCHUBStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Mappings
{
    public class ShipmentManagerProfile : Profile
    {
        public ShipmentManagerProfile()
        {
            // AllShipmentsViewModel.AllShipments Shipment

            CreateMap<Shipment, AllShipmentsViewModel>()
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.User.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.User.LastName))
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
            .ForMember(x => x.PurchaseDate, y => y.MapFrom(z => z.PurchaseDate))
            .ForMember(x => x.ShipmentDetails, y => y.MapFrom(z => z.ShipmentDetails))
            .ForMember(x => x.ShipmentTotalPrice, y => y.MapFrom(z => z.ShipmentProducts.Sum(x => x.Quantity * x.Product.Price)))
            .ForMember(x => x.ShippingCompany, y => y.MapFrom(z => z.ShippingCompany))
            .ForMember(x => x.ProductsCount, y => y.MapFrom(z => z.ShipmentProducts.Sum(x => x.Quantity)))
            .ForMember(x => x.ConfirmationStatus, y => y.MapFrom(z => z.ConfirmationStatus))
            .ForMember(x => x.Address, y => y.MapFrom(z => z.User.Address));

        }
    }
}
