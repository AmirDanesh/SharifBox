using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserApi.Domain.Repositories;
using UserApi.DTOs;

namespace UserApi.Application.Queries
{
    public class GetUsersName
    {
        public class Query : IRequest<List<UsersDropDownDTO>>
        {
            public Query(List<Guid> ids)
            {
                Ids = ids;
            }

            public List<Guid> Ids { get; }
        }

        public class Handler : IRequestHandler<Query, List<UsersDropDownDTO>>
        {
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;

            public Handler(IMapper mapper, IUserUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _userRepository = unitOfWork.UserRepository;
            }

            public async Task<List<UsersDropDownDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var a = await _userRepository.GetAllAsync();
                var data = await _userRepository.GetAll().Where(x => request.Ids.Contains(x.IdentityUserId)).ToListAsync();

                return _mapper.Map<List<UsersDropDownDTO>>(data);
            }
        }
    }
}
