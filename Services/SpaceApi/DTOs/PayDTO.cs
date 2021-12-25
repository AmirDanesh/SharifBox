using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.DTOs
{
    public class PayDTO
    {
        public Guid PaymentId { get; set; }
        public int Amount { get; set; }
    }
}
