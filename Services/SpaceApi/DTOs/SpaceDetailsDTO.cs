using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.DTOs
{
    public class SpaceDetailsDTO
    {
        public Guid SpaceId { get; set; }
        public string Title { get; set; }

        public Nullable<Guid> ParentId { get; set; }

        public string Description { get; set; }

        public byte Type { get; set; }

        public string TypeString { get; set; }

        public int Capacity { get; set; }

        public double Area { get; set; }

        public int NumOfVideoProjector { get; set; }

        public int NumOfChairs { get; set; }

        public int NumOfMicrophone { get; set; }

        public string SvgId { get; set; }
    }
}