using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Queries
{
    public class PasswordCheckQueryHandler : IRequestHandler<PasswordCheckQuery, SignInResult>
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<Domain.Models.User.IdentityUser> _signInManager;

        public PasswordCheckQueryHandler(IMediator mediator, SignInManager<Domain.Models.User.IdentityUser> signInManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;
        }

        public Task<SignInResult> Handle(PasswordCheckQuery request, CancellationToken cancellationToken)
        {
            return _signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, true);
        }
    }
}