using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.DTOs
{
    public class SpaceDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public byte TypeNumber { get; set; }

        public string Type { get; set; }

        public Nullable<Guid> ParentId { get; set; }

        public double Area { get; set; }

        public string Description { get; set; }

        public bool IsDisabled { get; set; }
    }
}