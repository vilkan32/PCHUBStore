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
                .ForMember(x => x.ProfilePictureUrl, y => y.MapFrom(z => z.ProfilePicture.Url))
                ;

        }
    }
}
