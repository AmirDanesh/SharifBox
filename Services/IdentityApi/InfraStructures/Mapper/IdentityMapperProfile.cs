using AutoMapper;
using IdentityApi.Application.Commands;
using IdentityApi.Domain.Models.User;
using IdentityApi.DTOs;

namespace IdentityApi.InfraStructures.Mapper
{
    public class IdentityMapperProfile : Profile
    {
        public IdentityMapperProfile()
        {
            CreateMap<RegisterUserCommand, IdentityUser>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<IdentityUser, UserDto>()
                .ReverseMap();
            CreateMap<IdentityUser, CreateUserCommand>()
                .ReverseMap()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<IdentityUser, UpdateUserCommand>()
                .ReverseMap()
                .ForMember(u => u.Id, opt => opt.Ignore())
                .ForMember(u => u.SecurityStamp, opt => opt.Ignore());
        }
    }
}