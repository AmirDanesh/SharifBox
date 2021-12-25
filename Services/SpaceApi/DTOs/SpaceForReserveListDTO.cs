using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.DTOs
{
    public class SpaceForReserveListDTO
    {
        public Guid SpaceId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Facilities { get; set; }
        public string Properties { get; set; }
    }
}
