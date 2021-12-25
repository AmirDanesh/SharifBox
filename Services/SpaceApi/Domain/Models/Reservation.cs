using SpaceApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.Domain.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string SvgId { get; set; }
        public Guid SpaceId { get; set; }
        public Space Space { get; set; }
        public SpaceType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid PaymentId { get; set; }
        public int PayAmount { get; set; }
        public Payment Payment { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ValidUntil { get; set; } = DateTime.UtcNow.AddMinutes(16);
        public bool IsFinalized { get; set; }
    }
}
