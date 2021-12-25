using MediatR;
using System;

namespace IdentityApi.Application.Queries
{
    public class GetUserByPhoneNumberQuery : IRequest<Domain.Models.User.IdentityUser>
    {
        public GetUserByPhoneNumberQuery(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public string PhoneNumber { get; set; }
    }
}