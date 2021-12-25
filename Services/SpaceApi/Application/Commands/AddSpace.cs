using AutoMapper;
using MediatR;
using SpaceApi.Application.Queries;
using SpaceApi.Domain.Enums;
using SpaceApi.Domain.Models;
using SpaceApi.Domain.Repositories;
using SpaceApi.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceApi.Application.Commands
{
    public class AddSpace
    {
        public class Command : IRequest<SpaceListDTO>
        {
            public Command(string title, string parentSvgId, string description, SpaceType type, int? capacity,
                double? area, int? numOfVideoProjector, int? numOfChairs, int? numOfMicrophone, string svgId)
            {
                Title = title;
                ParentSvgId = parentSvgId;
                Description = description;
                Type = type;
                Capacity = capacity ?? 0;
                Area = area ?? 0;
                NumOfVideoProjector = numOfVideoProjector ?? 0;
                NumOfChairs = numOfChairs ?? 0;
                NumOfMicrophone = numOfMicrophone ?? 0;
                SvgId = svgId;
            }

            [Required]
            public string Title { get; }

            public string ParentSvgId { get; }

            public string Description { get; }

            [Required]
            public SpaceType Type { get; }

            public int Capacity { get; set; }

            public double Area { get; }

            public int NumOfVideoProjector { get; }

            public int NumOfChairs { get; }

            public int NumOfMicrophone { get; }
            [Required]
            public string SvgId { get; }
        }

        public class Handler : IRequestHandler<Command, SpaceListDTO>
        {
            private readonly ISpaceUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly ISpaceRepository _spaceRepository;

            public Handler(ISpaceUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _mediator = mediator;
                _spaceRepository = unitOfWork.SpaceRepository;
            }

            public async Task<SpaceListDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var space = _mapper.Map<Space>(request);

                if (!string.IsNullOrEmpty(request.ParentSvgId))
                {
                    var parent = await _mediator.Send(new GetFullSpaceBySvgId.Query(request.ParentSvgId));
                    if (parent == null)
                        throw new NullReferenceException("فضای بالا دستی یافت نشد");
                    if (parent.Type != 0 && parent.Type != 1)
                        throw new NotSupportedException("فضای بالا دستی وارد شده نمی تواند زیر مجموعه داشته باشد");

                    space.ParentId = parent.SpaceId;
                }

                space = await _spaceRepository.AddAsync(space);

                switch (space.Type)
                {
                    case SpaceType.FixChair:
                    case SpaceType.FlexChair:
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

                await _unitOfWork.CommitAsync();

                return _mapper.Map<SpaceListDTO>(space);
            }
        }
    }
}
