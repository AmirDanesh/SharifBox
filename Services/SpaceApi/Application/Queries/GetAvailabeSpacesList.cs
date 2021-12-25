using MediatR;
using PersianDate;
using SpaceApi.Domain.Enums;
using SpaceApi.Domain.Repositories;
using SpaceApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceApi.Application.Queries
{
    public class GetAvailabeSpacesList
    {
        public class Query : IRequest<List<SpaceForReserveListDTO>>
        {
            public Query(string userName, SpaceType type, DateTime startDate, DateTime endDate)
            {
                UserName = userName;
                Type = type;
                StartDate = startDate;
                EndDate = endDate;
            }

            public string UserName { get; }
            public SpaceType Type { get; }
            public DateTime StartDate { get; }
            public DateTime EndDate { get; }
        }

        public class Handler : IRequestHandler<Query, List<SpaceForReserveListDTO>>
        {
            private readonly ISpaceUnitOfWork _unitOfWork;

            public Handler(ISpaceUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<List<SpaceForReserveListDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var spaces = await _unitOfWork.ReservationRepository
                                .SpacesForReserve(
                                        request.UserName,
                                        request.Type,
                                        request.StartDate,
                                        request.EndDate);

                var dto = new List<SpaceForReserveListDTO>();



                foreach (var item in spaces)
                {
                    switch (item.Type)
                    {
                        case SpaceType.FlexChair:
                            {
                                dto.Add(new SpaceForReserveListDTO()
                                {
                                    SpaceId = item.Id,
                                    Title = item.Title,
                                    Location = $"{item.Parent.Title}"
                                });
                                break;
                            }
                        case SpaceType.FixChair:
                            {
                                dto.Add(new SpaceForReserveListDTO()
                                {
                                    SpaceId = item.Id,
                                    Title = item.Title,
                                    Location = $"{item.Parent.Title}"
                                });
                                break;
                            }
                        case SpaceType.Room:
                            {
                                dto.Add(new SpaceForReserveListDTO()
                                {
                                    SpaceId = item.Id,
                                    Title = item.Title,
                                    Location = $"{item.Parent.Title}",
                                    Facilities = $"تعداد صندلی: {item.RentAbleRoom.NumOfChairs}",
                                    Properties = $"ظرفیت: {item.RentAbleRoom.Capacity} نفر، مساحت: {item.RentAbleRoom.Area} متر"
                                });
                                break;
                            }
                        case SpaceType.ConferenceRoom:
                            {
                                dto.Add(new SpaceForReserveListDTO()
                                {
                                    SpaceId = item.Id,
                                    Title = item.Title,
                                    Location = $"{item.Parent.Title}",
                                    Facilities = $"تعداد صندلی: {item.RentAbleConferenceRoom.NumOfChairs}، تعداد میکروفون: {item.RentAbleConferenceRoom.NumOfMicrophone}، تعداد ویدئو پرژکتور: {item.RentAbleConferenceRoom.NumOfVideoProjector}",
                                    Properties = $"ظرفیت: {item.RentAbleConferenceRoom.Capacity} نفر، مساحت: {item.RentAbleConferenceRoom.Area} متر"
                                });
                                break;
                            }
                    }
                }

                return dto;
            }
        }
    }
}
