using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpaceApi.Domain.Repositories;
using SpaceApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceApi.Application.Queries
{
    public class GetReservationByUserName
    {
        public class Query : IRequest<List<UserReservationListDTO>>
        {
            public Query(string userName)
            {
                UserName = userName;
            }

            public string UserName { get; }
        }

        public class Handler : IRequestHandler<Query, List<UserReservationListDTO>>
        {
            private readonly ISpaceUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(ISpaceUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<List<UserReservationListDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var reservations = await _unitOfWork
                    .ReservationRepository.GetAllIncluding(x => x.Space).Where(x => x.UserName == request.UserName).ToListAsync();

                return _mapper.Map<List<UserReservationListDTO>>(reservations);
            }
        }
    }
}
