using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Application.Queries
{
    public class PasswordCheckQuery : IRequest<SignInResult>
    {
        public PasswordCheckQuery(string username, string password, bool rememberMe)
        {
            UserName = username;
            Password = password;
            RememberMe = rememberMe;
        }

        [Required]
        [RegularExpression(@"^(09)\d{9}$")]
        public string UserName { get; }

        [Required]
        public string Password { get; }

        [Required]
        public bool RememberMe { get; }
    }
}