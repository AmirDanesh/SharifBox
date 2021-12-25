using AutoMapper;
using MediatR;
using SpaceApi.Application.Queries;
using SpaceApi.Domain.Enums;
using SpaceApi.Domain.Models;
using SpaceApi.Domain.Repositories;
using SpaceApi.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceApi.Application.Commands
{
    public class EditSpace
    {
        public class Command : IRequest<SpaceListDTO>
        {
            public Command(Guid spaceId, string title, Guid? parentId, string description, SpaceType type, int? capacity,
                double? area, int? numOfVideoProjector, int? numOfChairs, int? numOfMicrophone, string svgId)
            {
                SpaceId = spaceId;
                Title = title;
                ParentId = parentId;
                Description = description;
                Type = type;
                Capacity = capacity;
                Area = area;
                NumOfVideoProjector = numOfVideoProjector;
                NumOfChairs = numOfChairs;
                NumOfMicrophone = numOfMicrophone;
                SvgId = svgId;
            }

            public Guid SpaceId { get; }

            [Required]
            public string Title { get; }

            public Nullable<Guid> ParentId { get; }

            public string Description { get; }

            public SpaceType Type { get; }

            public Nullable<int> Capacity { get; set; }

            public double? Area { get; }

            public int? NumOfVideoProjector { get; }

            public int? NumOfChairs { get; }

            public int? NumOfMicrophone { get; }

            public string SvgId { get; }
        }

        public class Handler : IRequestHandler<Command, SpaceListDTO>
        {
            private readonly IMediator _mediator;
            private readonly ISpaceUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ISpaceRepository _spaceRepository;

            public Handler(IMediator mediator, ISpaceUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _spaceRepository = unitOfWork.SpaceRepository;
            }

            public async Task<SpaceListDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var space = await _spaceRepository.GetAsync(request.SpaceId);

                if (space.Type != request.Type)
                {
                    switch (space.Type)
                    {
                        case SpaceType.FlexChair:
                        case SpaceType.FixChair:
                            {
                                var chair = (await _unitOfWork.ChairRepository.FindByAsync(x => x.SpaceId == space.Id)).First();
                                _unitOfWork.ChairRepository.Delete(chair);
                                break;
                            }
                        case SpaceType.Room:
                            {
                                var room = (await _unitOfWork.RoomRepository.FindByAsync(x => x.SpaceId == space.Id)).First();
                                _unitOfWork.RoomRepository.Delete(room);
                                break;
                            }
                        case SpaceType.ConferenceRoom:
                            {
                                var conference = (await _unitOfWork.ConferenceRoomRepository.FindByAsync(x => x.SpaceId == space.Id)).First();
                                _unitOfWork.ConferenceRoomRepository.Delete(conference);
                                break;
                            }
                    }

                    switch (request.Type)
                    {
                        case SpaceType.FlexChair:
                        case SpaceType.FixChair:
                            {
                                var rentAble = _mapper.Map<RentAbleChair>(request);
                                rentAble.SpaceId = space.Id;
                                await _spaceRepository.AddChairAsync(rentAble);
                                break;
                            }
                        case SpaceType.Room:
                            {
                                var rentAble = _mapper.Map<RentAbleRoom>(request);
                                rentAble.SpaceId = space.Id;
                                await _spaceRepository.AddRoomAsync(rentAble);
                                break;
                            }
                        case SpaceType.ConferenceRoom:
                            {
                                var rentAble = _mapper.Map<RentAbleConferenceRoom>(request);
                                rentAble.SpaceId = space.Id;
                                await _spaceRepository.AddConferenceRoomAsync(rentAble);
                                break;
                            }
                    }

                }
                else
                {
                    switch (request.Type)
                    {
                        case SpaceType.FlexChair:
                        case SpaceType.FixChair:
                            {
                                var chair = (await _unitOfWork.ChairRepository.FindByAsync(x => x.SpaceId == request.SpaceId)).FirstOrDefault();
                                chair = _mapper.Map(request, chair);
                                await _unitOfWork.ChairRepository.UpdateAsync(chair, chair.Id);
                                break;
                            }
                        case SpaceType.Room:
                            {
                                var room = (await _unitOfWork.RoomRepository.FindByAsync(x => x.SpaceId == request.SpaceId)).FirstOrDefault();
                                room = _mapper.Map(request, room);
                                await _unitOfWork.RoomRepository.UpdateAsync(room, room.Id);
                                break;
                            }
                        case SpaceType.ConferenceRoom:
                            {
                                var conference = (await _unitOfWork.ConferenceRoomRepository.FindByAsync(x => x.SpaceId == request.SpaceId)).FirstOrDefault();
                                conference = _mapper.Map(request, conference);
                                await _unitOfWork.ConferenceRoomRepository.UpdateAsync(conference, conference.Id);
                                break;
                            }
                    }
                }

                space = _mapper.Map(request, space);

                await _spaceRepository.UpdateAsync(space, space.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<SpaceListDTO>(space);
            }
        }
    }
}