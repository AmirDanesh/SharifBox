using AutoMapper;
using MediatR;
using SpaceApi.Domain.Repositories;
using SpaceApi.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceApi.Application.Queries
{
    public class GetFullSpaceBySvgId
    {
        public class Query : IRequest<SpaceDetailsDTO>
        {
            public Query(string svgId)
            {
                SvgId = svgId;
            }

            [Required]
            public string SvgId { get; }
        }

        public class Handler : IRequestHandler<Query, SpaceDetailsDTO>
        {
            private readonly ISpaceUnitOfWork _spaceUnitOfWork;
            private readonly IMapper _mapper;

            private readonly ISpaceRepository _spaceRepository;

            public Handler(ISpaceUnitOfWork spaceUnitOfWork, IMapper mapper)
            {
                _spaceUnitOfWork = spaceUnitOfWork;
                _mapper = mapper;
                _spaceRepository = spaceUnitOfWork.SpaceRepository;
            }

            public async Task<SpaceDetailsDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var space = await _spaceRepository.GetSpaceAsync(request.SvgId);
                if (space == null)
                    throw new KeyNotFoundException("گزینه انتخاب شده هنوز ثبت نشده است");

                var dto = _mapper.Map<SpaceDetailsDTO>(space);

                if(space.Type == Domain.Enums.SpaceType.ConferenceRoom)
                {
                    var conferenceRoom = await _spaceRepository.GetConferenceRoomAsync(space.Id);
                    if(conferenceRoom == null)
                        throw new KeyNotFoundException("گزینه انتخاب شده هنوز ثبت نشده است");

                    dto.Area = conferenceRoom.Area;
                    dto.Capacity = conferenceRoom.Capacity;
                    dto.NumOfChairs = conferenceRoom.NumOfChairs;
                    dto.NumOfVideoProjector = conferenceRoom.NumOfVideoProjector;
                    dto.NumOfMicrophone = conferenceRoom.NumOfMicrophone;
                }
                else if(space.Type == Domain.Enums.SpaceType.Room)
                {
                    var room = await _spaceRepository.GetRoomAsync(space.Id);
                    if (room == null)
                        throw new KeyNotFoundException("گزینه انتخاب شده هنوز ثبت نشده است");

                    dto.Area = room.Area;
                    dto.Capacity = room.Capacity;
                    dto.NumOfChairs = room.NumOfChairs;
                }

                return dto;
            }
        }
    }
}
