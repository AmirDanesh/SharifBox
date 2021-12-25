using IdentityApi.Domain.Models.User;
using MediatR;
using System;

namespace IdentityApi.Application.Queries
{
    public class GetUserVerificationCodeQuery : IRequest<PhoneNumberVerifyCode>
    {
        public GetUserVerificationCodeQuery(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public string PhoneNumber { get; }
    }
}