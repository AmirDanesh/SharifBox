using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserApi.Domain.Repositories;
using UserApi.DTOs;

namespace UserApi.Application.Queries
{
    public class GetAllUsers
    {
        public class Query : IRequest<List<UsersListDTO>>
        {
            public Query()
            {

            }
        }

        public class Handler : IRequestHandler<Query, List<UsersListDTO>>
        {
            private readonly IUserUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUserUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<List<UsersListDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _unitOfWork.UserRepository.GetAllAsync();

                return _mapper.Map<List<UsersListDTO>>(users);
            }
        }
    }
}
