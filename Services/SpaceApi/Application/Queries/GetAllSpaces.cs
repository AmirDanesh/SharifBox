using AutoMapper;
using MediatR;
using SpaceApi.Domain.Repositories;
using SpaceApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceApi.Application.Queries
{
    public class GetAllSpaces
    {
        public class Query : IRequest<List<SpaceListDTO>>
        {
            public Query()
            {
            }
        }

        public class Handler : IRequestHandler<Query, List<SpaceListDTO>>
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

            public async Task<List<SpaceListDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var spaces = await _spaceRepository.GetAllAsync();

                var res = _mapper.Map<List<SpaceListDTO>>(spaces);

                return res;
            }
        }
    }
}