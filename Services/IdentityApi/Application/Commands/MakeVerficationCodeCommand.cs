using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Application.Commands
{
    public class MakeVerficationCodeCommand : IRequest<int>
    {
        public MakeVerficationCodeCommand(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        [Required]
        public string PhoneNumber { get; }
    }
}