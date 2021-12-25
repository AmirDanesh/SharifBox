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
    public class GetFullSpaceById
    {
        public class Query : IRequest<SpaceDetailsDTO>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            [Required]
            public Guid Id { get; }
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
                var space = await _spaceRepository.GetAsync(request.Id);
                if (space == null)
                    throw new KeyNotFoundException("گزینه انتخاب شده موجود نیست");

                var dto = _mapper.Map<SpaceDetailsDTO>(space);

                if (space.Type == Domain.Enums.SpaceType.ConferenceRoom)
                {
                    var conferenceRoom = await _spaceRepository.GetConferenceRoomAsync(space.Id);
                    if (conferenceRoom == null)
                        throw new KeyNotFoundException("گزینه انتخاب شده موجود نیست");

                    dto.Area = conferenceRoom.Area;
                    dto.Capacity = conferenceRoom.Capacity;
                    dto.NumOfChairs = conferenceRoom.NumOfChairs;
                    dto.NumOfVideoProjector = conferenceRoom.NumOfVideoProjector;
                    dto.NumOfMicrophone = conferenceRoom.NumOfMicrophone;
                }
                else if (space.Type == Domain.Enums.SpaceType.Room)
                {
                    var room = await _spaceRepository.GetRoomAsync(space.Id);
                    if (room == null)
                        throw new KeyNotFoundException("گزینه انتخاب شده موجود نیست");

                    dto.Area = room.Area;
                    dto.Capacity = room.Capacity;
                    dto.NumOfChairs = room.NumOfChairs;
                }

                return dto;
            }
        }
    }
}
