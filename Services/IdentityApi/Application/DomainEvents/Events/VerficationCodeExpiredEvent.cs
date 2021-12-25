using MediatR;
using System;

namespace IdentityApi.Application.DomainEvents.Events
{
    public class VerficationCodeExpiredEvent : INotification
    {
        public VerficationCodeExpiredEvent(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public string PhoneNumber { get; set; }
    }
}