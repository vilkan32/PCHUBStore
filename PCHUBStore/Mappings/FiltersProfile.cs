using AutoMapper;
using PCHUBStore.Data.Models;
using PCHUBStore.View.Models;
using PCHUBStore.View.Models.FilterViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCHUBStore.Mappings
{
    public class FiltersProfile : Profile
    {
        public FiltersProfile()
        {

            CreateMap<PCHUBStore.Data.Models.Filter, FilterViewModel>()
            .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
            .ForMember(x => x.Value, y => y.MapFrom(z => z.Value));

            CreateMap<FilterCategory, FilterCategoryViewModel>()
                  .ForMember(x => x.CategoryName, y => y.MapFrom(z => z.CategoryName))
                  .ForMember(x => x.ViewSubCategoryName, y => y.MapFrom(z => z.ViewSubCategoryName))
                    .ForMember(x => x.Filters, y => y.MapFrom(z => z.Filters));

        }

    }
}
