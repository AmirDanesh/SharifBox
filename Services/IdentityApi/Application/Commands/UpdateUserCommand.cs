using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Application.Commands
{
    public class UpdateUserCommand : IRequest<Domain.Models.User.IdentityUser>
    {
        public UpdateUserCommand(Guid id, string phoneNumber, string firstName, string lastName)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
        }

        [Required]
        public Guid Id { get; }

        [Required]
        [RegularExpression(@"^[0-9]*$")]
        public string PhoneNumber { get; }

        public string FirstName { get; }
        public string LastName { get; }
    }
}