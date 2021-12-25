using IdentityApi.Domain.Models.User;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Commands
{
    public class SetValidationCodeCommandHandler : IRequestHandler<SetValidationCodeCommand, PhoneNumberVerifyCode>
    {
        private readonly IDistributedCache _memoryCache;

        public SetValidationCodeCommandHandler(IDistributedCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<PhoneNumberVerifyCode> Handle(SetValidationCodeCommand request, CancellationToken cancellationToken)
        {
            var verficationCode = new PhoneNumberVerifyCode(request.PhoneNumber);
            var serializedObject = JsonConvert.SerializeObject(verficationCode);
            await _memoryCache.SetStringAsync(
                request.PhoneNumber,
                serializedObject,
                new DistributedCacheEntryOptions().SetAbsoluteExpiration(new System.TimeSpan(0, 5, 0)),
                cancellationToken);
            return verficationCode;
        }
    }
}