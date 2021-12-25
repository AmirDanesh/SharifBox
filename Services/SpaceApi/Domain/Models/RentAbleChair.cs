using System;

namespace SpaceApi.Domain.Models
{
    public class RentAbleChair : SpaceEntityBase
    {
        public Space Space { get; set; }

        public Guid SpaceId { get; set; }
    }
}