using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Application.Commands
{
    public class SetPassWordCommand : IRequest<Guid>
    {
        public SetPassWordCommand(string password, string phoneNumber)
        {
            Password = password;
            PhoneNumber = phoneNumber;
        }

        [Required]
        [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$")]
        public string Password { get; }

        [Required]
        public string PhoneNumber { get; }
    }
}