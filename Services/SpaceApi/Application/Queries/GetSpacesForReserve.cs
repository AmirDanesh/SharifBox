using MediatR;
using PersianDate;
using SpaceApi.Domain.Enums;
using SpaceApi.Domain.Models;
using SpaceApi.Domain.Repositories;
using SpaceApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceApi.Application.Queries
{
    public class GetSpacesForReserve
    {
        public class Query : IRequest<List<SpaceForReserveListDTO>>
        {
            public Query(string userName, SpaceType type, string startDate, string endDate, string startTime, string endTime)
            {
                UserName = userName;
                Type = type;
                StartDate = startDate;
                EndDate = endDate;
                StartTime = startTime;
                EndTime = endTime;
            }

            public string UserName { get; }
            public SpaceType Type { get; }
            public string StartDate { get; }
            public string EndDate { get; }
            public string StartTime { get; }
            public string EndTime { get; }
        }

        public class Handler : IRequestHandler<Query, List<SpaceForReserveListDTO>>
        {
            private readonly ISpaceUnitOfWork _unitOfWork;
            private readonly IMediator _mediator;

            public Handler(ISpaceUnitOfWork unitOfWork, IMediator mediator)
            {
                _unitOfWork = unitOfWork;
                _mediator = mediator;
            }

            public async Task<List<SpaceForReserveListDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request.Type == SpaceType.FixChair || request.Type == SpaceType.Room)
                {
                    return await _mediator.Send(new GetAvailabeSpacesList.Query(
                            request.UserName,
                            request.Type,
                            PersianDateTime.ConvertToDateTime(request.StartDate),
                            PersianDateTime.ConvertToDateTime(request.EndDate)));
                }
                else
                {
                    var startTime = Convert.ToDateTime(request.StartTime);
                    var endTime = Convert.ToDateTime(request.EndTime);

                    var startDate = PersianDateTime.ConvertToDateTime(request.StartDate).Add(new TimeSpan(startTime.Hour, startTime.Minute,startTime.Second));
                    var endDate = PersianDateTime.ConvertToDateTime(request.StartDate).Add(new TimeSpan(endTime.Hour, endTime.Minute, endTime.Second));

                    return await _mediator.Send(new GetAvailabeSpacesList.Query(
                            request.UserName,
                            request.Type,
                            startDate,
                            endDate));
                }
            }
        }
    }
}
