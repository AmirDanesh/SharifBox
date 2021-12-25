using SpaceApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.Domain.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public Nullable<DateTime> PaymentDate { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public PaymentStatus Status { get; set; }
        public string Description { get; set; }
    }
}
