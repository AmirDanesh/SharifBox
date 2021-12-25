using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Application.Queries
{
    public class GetUserByUserNameQuery : IRequest<Domain.Models.User.IdentityUser>
    {
        public GetUserByUserNameQuery(string username)
        {
            UserName = username;
        }

        [Required]
        [RegularExpression(@"^(09)\d{9}$")]
        public string UserName { get; }
    }
}