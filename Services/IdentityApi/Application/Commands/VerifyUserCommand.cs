using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Application.Commands
{
    public class VerifyUserCommand : IRequest<IdentityResult>
    {
        public VerifyUserCommand(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        [Required]
        public string PhoneNumber { get; }
    }
}