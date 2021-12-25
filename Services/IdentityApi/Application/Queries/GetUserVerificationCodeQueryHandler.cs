using IdentityApi.Domain.Models.User;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Queries
{
    public class GetUserVerificationCodeQueryHandler : IRequestHandler<GetUserVerificationCodeQuery, PhoneNumberVerifyCode>
    {
        private readonly IDistributedCache _memoryCache;

        public GetUserVerificationCodeQueryHandler(IDistributedCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<PhoneNumberVerifyCode> Handle(GetUserVerificationCodeQuery request, CancellationToken cancellationToken)
        {
            var code = await _memoryCache.GetStringAsync(request.PhoneNumber, cancellationToken);
            if (!string.IsNullOrWhiteSpace(code))
            {
                PhoneNumberVerifyCode value = JsonConvert.DeserializeObject<PhoneNumberVerifyCode>(code);
                return value;
            }
            return null;
        }
    }
}