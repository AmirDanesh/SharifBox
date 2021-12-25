using AutoMapper;
using System.Linq;
using TeamApi.Application.Commands;
using TeamApi.Domain.Models;
using TeamApi.DTOs;
using System.Collections.Generic;
using System;

namespace TeamApi.InfraStructures.Mapper
{
    public class TeamMapperProfile : Profile
    {
        public TeamMapperProfile()
        {
            CreateMap<Team, TeamDTO>()
            .ConstructUsing(x => new TeamDTO(
                x.Id,
                x.Name,
                new KeyValuePair<Guid, string>(x.ManagerUserId, ""),

                x.TeamDetails.ActivityField,
                x.TeamDetails.Description
            )).ReverseMap();

            CreateMap<AddTeam.Command, Team>()
            .ForMember(x => x.TeamDetails, opt => opt.MapFrom(s => new TeamDetails()
            {
                ActivityField = s.ActivityField,
                Description = s.Description
            }));

            CreateMap<EditTeam.Command, Team>()
                .ForPath(x => x.TeamDetails.ActivityField, opt => opt.MapFrom(s => s.ActivityField))
                .ForPath(x => x.TeamDetails.Description, opt => opt.MapFrom(s => s.Description))
                .ReverseMap();

            CreateMap<Team, TeamListDTO>()
                .ForMember(x => x.NumberOfUsers, opt => opt.MapFrom(s => s.TeamUsers.Count()));
        }
    }
}