using AutoMapper;
using News.Api.DTOs;
using NewsApi.Application.Commands;
using NewsApi.Domain.Models;
using NewsApi.DTOs;
using PersianDate;

namespace NewsApi.InfraStructures.Mapper
{
    public class EventMapperProfile : Profile
    {
        public EventMapperProfile()
        {
            CreateMap<AddEvent.Command, Event>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(s => PersianDateTime.ConvertToNullableDateTime(s.StartDate, "1901/01/01")))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(s => PersianDateTime.ConvertToNullableDateTime(s.EndDate, "1901/01/01")));

            CreateMap<EditEvent.Command, Event>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(s => PersianDateTime.ConvertToNullableDateTime(s.StartDate, "1901/01/01")))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(s => PersianDateTime.ConvertToNullableDateTime(s.EndDate, "1901/01/01")));

            CreateMap<Event, EventsForLanding>()
            .ConstructUsing(x =>
                new EventsForLanding(
                    x.Id,
                    x.Title
                ));

            CreateMap<Event, EventDTO>()
                .ConstructUsing(x =>
                   new EventDTO(x.Id,
                                x.Title,
                                x.Content,
                                new PersianDateTime(x.StartDate).ToString("yyyy/MM/dd", null),
                                new PersianDateTime(x.EndDate).ToString("yyyy/MM/dd", null),
                                x.ShowOnLanding))
                .ReverseMap()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(s => PersianDateTime.ConvertToNullableDateTime(s.StartDate, "1901/01/01")))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(s => PersianDateTime.ConvertToNullableDateTime(s.EndDate, "1901/01/01")));
        }
    }
}