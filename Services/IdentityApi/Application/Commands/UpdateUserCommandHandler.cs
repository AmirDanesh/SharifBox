using AutoMapper;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Domain.Models.User.IdentityUser>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly UserManager<Domain.Models.User.IdentityUser> _userManager;
        private readonly EventBusRabbitMQProducer _eventBus;

        public UpdateUserCommandHandler(IMapper mapper, IMediator mediator, UserManager<Domain.Models.User.IdentityUser> userManager, EventBusRabbitMQProducer eventBus)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;
            _eventBus = eventBus;
        }

        public async Task<Domain.Models.User.IdentityUser> Handle(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(updateUserCommand.Id.ToString());
            user = _mapper.Map(updateUserCommand, user);
            var res = await _userManager.UpdateAsync(user);

            if (res.Succeeded)
            {
                _eventBus.PublishIdentityUserCreated(EventBusConstants.IdentityUserCreatedQueue,
                    new IdentityUserCreatedEvent()
                    {
                        IdentityUserId = user.Id,
                        FirstName = updateUserCommand.FirstName,
                        LastName = updateUserCommand.LastName,
                        PhoneNumber = user.PhoneNumber
                    });

                return user;
            }
            throw new InvalidOperationException(res.Errors.First().Description);
        }
    }
}