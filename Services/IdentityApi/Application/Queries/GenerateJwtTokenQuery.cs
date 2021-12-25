using IdentityApi.Domain.Models.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IdentityApi.Application.Queries
{
    public class GenerateJwtTokenQuery : IRequest<string>
    {
        public GenerateJwtTokenQuery(Domain.Models.User.IdentityUser user, List<string> roles)
        {
            User = user;
            Roles = roles;
        }

        public Domain.Models.User.IdentityUser User { get; }
        public List<string> Roles { get; }
    }
}