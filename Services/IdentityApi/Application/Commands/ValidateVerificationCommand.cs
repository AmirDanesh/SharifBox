using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Application.Commands
{
    public class ValidateVerificationCommand : IRequest<string>
    {
        public ValidateVerificationCommand(string phoneNumber, int verificationCode)
        {
            PhoneNumber = phoneNumber;
            VerificationCode = verificationCode;
        }

        [Required]
        public string PhoneNumber { get; }

        [Required]
        [RegularExpression(@"^[0-9]{6}$")]
        public int VerificationCode { get; }
    }
}