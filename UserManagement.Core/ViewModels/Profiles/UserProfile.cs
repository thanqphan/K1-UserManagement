using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;
using UserManagement.Core.ViewModels.Requests.User;
using UserManagement.Core.ViewModels.Responses;

namespace UserManagement.Core.ViewModels.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UserUpdateRequest, User>()
                .ForMember(dest => dest.LastUpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !(srcMember is string str && string.IsNullOrEmpty(str))));
        }
    }
}
