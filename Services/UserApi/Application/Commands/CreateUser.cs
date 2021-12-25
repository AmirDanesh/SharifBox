using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserApi.Application.Queries;
using UserApi.Domain.Models.User;
using UserApi.Domain.Repositories;
using UserApi.DTOs;
using UserApi.InfraStructures.Mapper;

namespace UserApi.Application.Commands
{
    public class CreateUser
    {
        public class Command : IRequest<UserProfileDTO>
        {
            public Command(Guid identityUserId, string firstName, string lastName, string phoneNumber)
            {
                IdentityUserId = identityUserId;
                FirstName = firstName;
                LastName = lastName;
                PhoneNumber = phoneNumber;
            }

            public Guid IdentityUserId { get; }

            public string FirstName { get; }

            public string LastName { get; }

            public string PhoneNumber { get; }
        }

        public class Handler : IRequestHandler<Command, UserProfileDTO>
        {
            private readonly IUserUnitOfWork _unitOfWork;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(IUserUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
            {
                _unitOfWork = unitOfWork;
                _userRepository = unitOfWork.UserRepository;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<UserProfileDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var userDto = await _mediator.Send(new GetUserProfile.Query(request.IdentityUserId));

                if(userDto != null)
                {
                    var existingUser = _mapper.Map<User>(userDto);
                    existingUser.FirstName = request.FirstName;
                    existingUser.LastName = request.LastName;

                    await _userRepository.UpdateAsync(existingUser, existingUser.Id);

                    return _mapper.Map<UserProfileDTO>(existingUser);
                }
                else
                {
                    var user = _mapper.Map<User>(request);

                    user = await _userRepository.AddAsync(user);
                    await _unitOfWork.CommitAsync();

                    return _mapper.Map<UserProfileDTO>(user);
                }
            }
        }
    }
}