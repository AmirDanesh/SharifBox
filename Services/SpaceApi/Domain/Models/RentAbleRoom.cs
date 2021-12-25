using System;

namespace SpaceApi.Domain.Models
{
    public class RentAbleRoom : SpaceEntityBase
    {
        public double Area { get; set; }

        public int Capacity { get; set; }

        public int NumOfChairs { get; set; }

        public Space Space { get; set; }

        public Guid SpaceId { get; set; }
    }
}