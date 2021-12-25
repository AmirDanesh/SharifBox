using AutoMapper;
using IdentityApi.Application.Queries;
using IdentityApi.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly UserManager<Domain.Models.User.IdentityUser> _userManager;
        private readonly IIdentityUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IMapper mapper, IMediator mediator, UserManager<Domain.Models.User.IdentityUser> userManager, IIdentityUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var createUserCommand = new CreateUserCommand(request.FirstName, request.LastName, request.PhoneNumber);
                var res = await _mediator.Send(createUserCommand);
                // TODO should be an event
                var verificationCode = await _mediator.Send(new MakeVerficationCodeCommand(res.PhoneNumber));
                return res.Id;
            }
            catch (InvalidOperationException e)
            {
                var user = await _mediator.Send(new GetUserByUserNameQuery(request.PhoneNumber));
                if (user.PhoneNumberConfirmed)
                    throw new InvalidOperationException(e.Message);

                var res = await _mediator.Send(new UpdateUserCommand(user.Id, user.PhoneNumber, request.FirstName, request.LastName));
                var verificationCode = await _mediator.Send(new MakeVerficationCodeCommand(res.PhoneNumber));
                return res.Id;
            }
        }
    }
}