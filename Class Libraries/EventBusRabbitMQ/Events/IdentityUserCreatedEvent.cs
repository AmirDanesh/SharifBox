using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Events
{
    public class IdentityUserCreatedEvent
    {
        public Guid IdentityUserId { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}