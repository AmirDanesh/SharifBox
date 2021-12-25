using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.DTOs
{
    public class UserPaymentsListDTO
    {
        public string PayDate { get; set; }
        public string Status { get; set; }
        public string PayFor { get; set; }
        public int Amount { get; set; }
    }
}
