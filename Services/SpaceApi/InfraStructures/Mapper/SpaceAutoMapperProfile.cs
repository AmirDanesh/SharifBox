using AutoMapper;
using PersianDate;
using Shared;
using SpaceApi.Application.Commands;
using SpaceApi.Domain.Enums;
using SpaceApi.Domain.Models;
using SpaceApi.DTOs;

namespace SpaceApi.InfraStructures.Mapper
{
    public class SpaceAutoMapperProfile : Profile
    {
        public SpaceAutoMapperProfile()
        {
            CreateMap<AddSpace.Command, Space>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Space, SpaceListDTO>()
                .ForMember(x => x.Type, opt => opt.MapFrom(s => EnumUtility.GetDisplayValue(s.Type)))
                .ForMember(x => x.TypeNumber, opt => opt.MapFrom(s => s.Type))
                .ReverseMap();

            CreateMap<Space, SpaceDTO>()
                .ForMember(x => x.Type, opt => opt.MapFrom(s => EnumUtility.GetDisplayValue(s.Type)))
                .ForMember(x => x.TypeNumber, opt => opt.MapFrom(s => s.Type))
                .ReverseMap();

            CreateMap<Space, SpaceDetailsDTO>()
                .ForMember(x => x.SpaceId, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.TypeString, opt => opt.MapFrom(s => EnumUtility.GetDisplayValue(s.Type)))
                .ReverseMap();

            CreateMap<AddSpace.Command, RentAbleChair>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AddSpace.Command, RentAbleRoom>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AddSpace.Command, RentAbleConferenceRoom>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Pay.Command, Reservation>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(s => PersianDateTime.ConvertToDateTime(s.StartDate, "1901/01/01")))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(s => PersianDateTime.ConvertToDateTime(s.EndDate, "1901/01/01")))
                .ForMember(x => x.PayAmount, opt => opt.MapFrom(s => s.Amount));

            CreateMap<CreateReserve.Command, Reservation>();

            CreateMap<CreatePayment.Command, Payment>()
                .ForMember(x => x.Status, opt => opt.MapFrom(s => PaymentStatus.Pending))
                .ForMember(x => x.Description, opt => opt.MapFrom(s => s.Type));
            CreateMap<Payment, PayDTO>();

            CreateMap<Reservation, UserReservationListDTO>()
                .ForMember(x => x.Type, opt => opt.MapFrom(s => EnumUtility.GetDisplayValue(s.Space.Type)))
                .ForMember(x => x.ReserveDate, opt => opt.MapFrom(s => new PersianDateTime(s.CreatedDate).ToString("yyyy/MM/dd", null)))
                .ForMember(x => x.StartDate, opt => opt.MapFrom(s => new PersianDateTime(s.StartDate).ToString("yyyy/MM/dd", null)))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(s => new PersianDateTime(s.EndDate).ToString("yyyy/MM/dd", null)));

            CreateMap<Payment, UserPaymentsListDTO>()
                .ForMember(x => x.PayDate, opt => opt.MapFrom(s => new PersianDateTime(s.PaymentDate).ToString("yyyy/MM/dd", null)))
                .ForMember(x => x.Status, opt => opt.MapFrom(s => EnumUtility.GetDisplayValue(s.Status)))
                .ForMember(x => x.PayFor, opt => opt.MapFrom(s => s.Description));

            CreateMap<EditSpace.Command, RentAbleChair>()
               .ForMember(x => x.Id, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<EditSpace.Command, RentAbleRoom>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<EditSpace.Command, RentAbleConferenceRoom>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<EditSpace.Command, Space>()
                .ForMember(x => x.Id, opt => opt.MapFrom(s => s.SpaceId));
        }
    }
}
