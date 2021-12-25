using IdentityApi.Application.DomainEvents.Events;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.DomainEvents.EventHandlers
{
    public class RemoveVerificationCodeHandler : INotificationHandler<VerficationCodeExpiredEvent>
    {
        private readonly IDistributedCache _cache;

        public RemoveVerificationCodeHandler(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task Handle(VerficationCodeExpiredEvent notification, CancellationToken cancellationToken)
        {
            await _cache.RemoveAsync(notification.PhoneNumber, cancellationToken);
        }
    }
}