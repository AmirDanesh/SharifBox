using AutoMapper;
using MediatR;
using SpaceApi.Domain.Models;
using SpaceApi.Domain.Repositories;
using SpaceApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceApi.Application.Queries
{
    public class GetSpaceById
    {
        public class Query : IRequest<SpaceDTO>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; }
        }

        public class Handler : IRequestHandler<Query, SpaceDTO>
        {
            private readonly ISpaceUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(ISpaceUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<SpaceDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var space =  await _unitOfWork.SpaceRepository.GetAsync(request.Id);

                if (space != null)
                    return _mapper.Map<SpaceDTO>(space);
                else
                    throw new NullReferenceException("موجود نیست");
            }
        }
    }
}
