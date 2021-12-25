using AutoMapper;
using EventBusRabbitMQ.Events;
using PersianDate;
using System.Linq;
using UserApi.Application.Commands;
using UserApi.Domain.Models.User;
using UserApi.DTOs;

namespace UserApi.InfraStructures.Mapper
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<CreateUser.Command, User>();
            CreateMap<CreateUser.Command, IdentityUserCreatedEvent>().ReverseMap();
            CreateMap<EditUserProfile.Command, User>();
            CreateMap<EditUserProfile.Command, UserProfileDTO>();

            CreateMap<UserProfileDTO, User>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.UserId))
                .ReverseMap()
                .ConstructUsing(p => new UserProfileDTO(p.Id, p.IdentityUserId, p.FirstName, p.LastName, p.Address, p.NationalCode, (p.UserSkills != null) ? p.UserSkills.Select(x => x.SkillId) : null));

            CreateMap<Skill, SkillDropDownDTO>();

            CreateMap<User, UsersDropDownDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.IdentityUserId))
                .ForMember(x => x.Name, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));

            CreateMap<User, UsersListDTO>()
                .ForMember(x => x.JoinDate, opt => opt.MapFrom(s => new PersianDateTime(s.JoinDate).ToString("yyyy/MM/dd", null)));
        }
    }
}