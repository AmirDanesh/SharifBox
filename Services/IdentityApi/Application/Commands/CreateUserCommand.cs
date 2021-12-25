using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Application.Commands
{
    public class CreateUserCommand : IRequest<Domain.Models.User.IdentityUser>
    {
        public CreateUserCommand(string firstName, string lastName, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

        [Required]
        [RegularExpression(@"^[a-zA-Zآ-ی ء چ]+$")]
        public string FirstName { get; }

        [Required]
        [RegularExpression(@"^[a-zA-Zآ-ی ء چ]+$")]
        public string LastName { get; }

        [Required]
        [RegularExpression(@"^(09)\d{9}$")]
        public string PhoneNumber { get; }
    }
}