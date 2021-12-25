using IdentityApi.Domain.Models.User;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Application.Commands
{
    public class SetValidationCodeCommand : IRequest<PhoneNumberVerifyCode>
    {
        public SetValidationCodeCommand(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        [Required]
        public string PhoneNumber { get; }
    }
}