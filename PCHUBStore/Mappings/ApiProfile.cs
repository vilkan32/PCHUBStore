using AutoMapper;
using PCHUBStore.Data.Models;
using PCHUBStore.View.Models.ApiViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Mappings
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            //ApiProductHistoryViewModel

            //          from          to
            CreateMap<Product, ApiProductHistoryViewModel>()
                .ForMember(x => x.PictureUrl, y => y.MapFrom(z => z.MainPicture.Url))
                  .ForMember(x => x.Price, y => y.MapFrom(z => z.Price))
                    .ForMember(x => x.Title, y => y.MapFrom(z => z.Make))
                      .ForMember(x => x.VisitProductUrl, y => y.MapFrom(z => "/Products/" + z.Category.Name + "/" + z.Id));
        }
    }
}
