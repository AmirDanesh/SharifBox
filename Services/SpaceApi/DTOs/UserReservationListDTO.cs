using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.DTOs
{
    public class UserReservationListDTO
    {
        public string Type { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int PayAmount { get; set; }
        public string ReserveDate { get; set; }
    }
}
