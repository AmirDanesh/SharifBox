using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Commands
{
    public class VerifyUserCommandHandler : IRequestHandler<VerifyUserCommand, IdentityResult>
    {
        private readonly UserManager<Domain.Models.User.IdentityUser> _userManager;
        private readonly IMediator _mediator;

        public VerifyUserCommandHandler(UserManager<Domain.Models.User.IdentityUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<IdentityResult> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.PhoneNumber);
            user.PhoneNumberConfirmed = true;
            return await _userManager.UpdateAsync(user);
        }
    }
}