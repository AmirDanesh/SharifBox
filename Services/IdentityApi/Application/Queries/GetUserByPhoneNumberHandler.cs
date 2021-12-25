using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Queries
{
    public class GetUserByPhoneNumberHandler : IRequestHandler<GetUserByPhoneNumberQuery, Domain.Models.User.IdentityUser>
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<Domain.Models.User.IdentityUser> _userManager;

        public GetUserByPhoneNumberHandler(UserManager<Domain.Models.User.IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Domain.Models.User.IdentityUser> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.FindByNameAsync(request.PhoneNumber);
        }
    }
}