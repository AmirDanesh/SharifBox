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
    public class GetPaymentsByUserName
    {
        public class Query : IRequest<List<UserPaymentsListDTO>>
        {
            public Query(string userName)
            {
                UserName = userName;
            }

            public string UserName { get; }
        }

        public class Handler : IRequestHandler<Query, List<UserPaymentsListDTO>>
        {
            private readonly ISpaceUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(ISpaceUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<List<UserPaymentsListDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var payments = await _unitOfWork.PaymentRepository.FindAllAsync(x => x.UserName == request.UserName);

                return _mapper.Map<List<UserPaymentsListDTO>>(payments);
            }
        }
    }
}
