using AutoMapper;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using IdentityApi.Application.DomainEvents.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Domain.Models.User.IdentityUser>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly UserManager<Domain.Models.User.IdentityUser> _userManager;
        private readonly EventBusRabbitMQProducer _eventBus;

        public CreateUserCommandHandler(IMapper mapper, IMediator mediator, UserManager<Domain.Models.User.IdentityUser> userManager, EventBusRabbitMQProducer eventBus)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;
            _eventBus = eventBus;
        }

        public async Task<Domain.Models.User.IdentityUser> Handle(CreateUserCommand createUserCommand, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Models.User.IdentityUser>(createUserCommand);
            var res = await _userManager.CreateAsync(user);
            user = await _userManager.FindByNameAsync(user.PhoneNumber);
            await _userManager.AddToRoleAsync(user, "admin");

            if (res.Succeeded)
            {
                _eventBus.PublishIdentityUserCreated(EventBusConstants.IdentityUserCreatedQueue,
                    new IdentityUserCreatedEvent()
                    {
                        PhoneNumber = createUserCommand.PhoneNumber,
                        IdentityUserId = user.Id,
                        FirstName = createUserCommand.FirstName,
                        LastName = createUserCommand.LastName
                    });

                return user;
            }
            throw new InvalidOperationException(res.Errors.FirstOrDefault().Description);
        }
    }
}