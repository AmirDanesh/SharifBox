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
    public class GetUserProfileByUserName
    {
        public class Query : IRequest<UserProfileDTO>
        {
            public Query(string phoneNumber)
            {
                PhoneNumber = phoneNumber;
            }

            public string PhoneNumber { get; }
        }

        public class QueryHandler : IRequestHandler<Query, UserProfileDTO>
        {
            private readonly IMapper _mapper;
            private readonly IUserUnitOfWork _unitOfWork;
            private readonly IUserRepository _userRepository;
            private readonly IUserSkillRepository _userSkillRepository;

            public QueryHandler(IMapper mapper, IUserUnitOfWork unitOfWork)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _userRepository = unitOfWork.UserRepository;
                _userSkillRepository = unitOfWork.UserSkillRepository;
            }

            public async Task<UserProfileDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userRepository
                    .GetAllIncluding(x => x.UserSkills)
                    .FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

                var skills = (await _userSkillRepository.GetAllAsync())
                    .Where(x => x.UserId == user.Id)
                    .Select(x => x.SkillId);

                return _mapper.Map<UserProfileDTO>(user);
            }
        }
    }
}
