using IdentityApi.Application.DomainEvents.Events;
using IdentityApi.Application.Queries;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Commands
{
    public class MakeVerficationCodeCommandHandler : IRequestHandler<MakeVerficationCodeCommand, int>
    {
        private readonly IMediator _mediator;

        public MakeVerficationCodeCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> Handle(MakeVerficationCodeCommand request, CancellationToken cancellationToken)
        {
            var verifCode = await _mediator.Send(new GetUserVerificationCodeQuery(request.PhoneNumber));
            if (verifCode == null)
            {
                return (await _mediator.Send(new SetValidationCodeCommand(request.PhoneNumber))).VerifyCode;
            }
            else
            {
                if (verifCode.ExpireDate < DateTime.UtcNow || verifCode.ExpireDate.AddMinutes(-2) < DateTime.UtcNow)
                {
                    await _mediator.Publish(new VerficationCodeExpiredEvent(request.PhoneNumber));
                    return (await _mediator.Send(new SetValidationCodeCommand(request.PhoneNumber))).VerifyCode;
                }
                else
                {
                    return verifCode.VerifyCode;
                }
            }
        }
    }
}