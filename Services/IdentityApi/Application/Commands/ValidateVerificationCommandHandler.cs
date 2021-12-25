using IdentityApi.Application.Queries;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Commands
{
    public class ValidateVerificationCommandHandler : IRequestHandler<ValidateVerificationCommand, string>
    {
        private readonly IMediator _mediator;

        public ValidateVerificationCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Handle(ValidateVerificationCommand request, CancellationToken cancellationToken)
        {
            var verficationCode = await _mediator.Send(new GetUserVerificationCodeQuery(request.PhoneNumber));
            if (verficationCode == null)
            {
                throw new NotSupportedException("کد احراز هویت شما منقضی شده است");
            }
            else if (verficationCode.VerifyCode == request.VerificationCode && verficationCode.ExpireDate > DateTime.UtcNow)
            {
                var res = await _mediator.Send(new VerifyUserCommand(request.PhoneNumber));
                if (res.Succeeded)
                {
                    var user = await _mediator.Send(new GetUserByPhoneNumberQuery(request.PhoneNumber));
                    var userRoles = await _mediator.Send(new GetUserRoles.Query(request.PhoneNumber));
                    return await _mediator.Send(new GenerateJwtTokenQuery(user, userRoles));
                }
                else
                {
                    throw new InvalidOperationException(res.Errors.First().Description);
                }
            }
            else
            {
                throw new NotSupportedException("کد نا معتبر");
            }
        }
    }
}