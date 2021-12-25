using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Queries
{
    public class GetUserByUserNameQueryHandler : IRequestHandler<GetUserByUserNameQuery, Domain.Models.User.IdentityUser>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Domain.Models.User.IdentityUser> _userManager;

        public GetUserByUserNameQueryHandler(UserManager<Domain.Models.User.IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Domain.Models.User.IdentityUser> Handle(GetUserByUserNameQuery userDto, CancellationToken cancellationToken)
        {
            return await _userManager.FindByNameAsync(userDto.UserName);
        }
    }
}