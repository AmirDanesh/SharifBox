using IdentityApi.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Commands
{
    public class SetPassWordCommandHandler : IRequestHandler<SetPassWordCommand, Guid>
    {
        private readonly UserManager<Domain.Models.User.IdentityUser> _userManager;
        private readonly IMediator _mediator;

        public SetPassWordCommandHandler(UserManager<Domain.Models.User.IdentityUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(SetPassWordCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByPhoneNumberQuery(request.PhoneNumber));

            if (await _userManager.HasPasswordAsync(user))
            {
                await _userManager.RemovePasswordAsync(user);
            }

            var res = await _userManager.AddPasswordAsync(user, request.Password);
            if (res.Succeeded)
            {
                return user.Id;
            }
            throw new Exception(res.Errors.First().Description);
        }
    }
}