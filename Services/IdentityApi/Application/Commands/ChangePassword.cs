using IdentityApi.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Commands
{
    public class ChangePassword
    {
        public class Command : IRequest<IdentityResult>
        {
            public Command(string phoneNumber, string currentPassword, string newPassword)
            {
                PhoneNumber = phoneNumber;
                CurrentPassword = currentPassword;
                NewPassword = newPassword;
            }

            public string PhoneNumber { get; }

            public string CurrentPassword { get; }

            public string NewPassword { get; }
        }

        public class Handler : IRequestHandler<Command, IdentityResult>
        {
            private readonly UserManager<Domain.Models.User.IdentityUser> _userManager;
            private readonly IMediator _mediator;

            public Handler(UserManager<Domain.Models.User.IdentityUser> userManager, IMediator mediator)
            {
                _userManager = userManager;
                _mediator = mediator;
            }

            public async Task<IdentityResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _mediator.Send(new GetUserByPhoneNumberQuery(request.PhoneNumber));
                var res = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
                if (res.Succeeded)
                {
                    return res;
                }
                throw new Exception(res.Errors.First().Description);
            }
        }
    }
}