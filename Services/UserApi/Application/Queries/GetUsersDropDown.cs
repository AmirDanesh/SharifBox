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
    public class GetUsersDropDown
    {
        public class Query : IRequest<List<UsersDropDownDTO>>
        {
        }

        public class Handler : IRequestHandler<Query, List<UsersDropDownDTO>>
        {
            private readonly IMapper _mapper;
            private readonly IUserUnitOfWork _unitOfWork;
            private readonly IUserRepository _userRepository;

            public Handler(IMapper mapper, IUserUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _userRepository = unitOfWork.UserRepository;
            }

            public async Task<List<UsersDropDownDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetAllAsync();

                return _mapper.Map<List<UsersDropDownDTO>>(users);
            }
        }
    }
}