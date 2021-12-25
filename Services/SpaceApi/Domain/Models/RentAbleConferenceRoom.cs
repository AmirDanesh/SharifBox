using System;

namespace SpaceApi.Domain.Models
{
    public class RentAbleConferenceRoom : SpaceEntityBase
    {
        public double Area { get; set; }

        public int Capacity { get; set; }

        public int NumOfChairs { get; set; }

        public int NumOfVideoProjector { get; set; }

        public int NumOfMicrophone { get; set; }

        public Space Space { get; set; }

        public Guid SpaceId { get; set; }
    }
}