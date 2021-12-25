using IdentityApi.Domain.Models.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityApi.Application.Queries
{
    public class GetUserRoles
    {
        public class Query : IRequest<List<string>>
        {
            public Query(string userName)
            {
                UserName = userName;
            }

            public string UserName { get; }
        }

        public class Handler : IRequestHandler<Query, List<string>>
        {
            private readonly UserManager<Domain.Models.User.IdentityUser> _userManager;
            private readonly IMediator _mediator;

            public Handler(UserManager<Domain.Models.User.IdentityUser> userManager, IMediator mediator)
            {
                _userManager = userManager;
                _mediator = mediator;
            }
            public async Task<List<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _mediator.Send(new GetUserByUserNameQuery(request.UserName));
                var roles =  (await _userManager.GetRolesAsync(user)).ToList();
                return roles;
            }
        }
    }
}
