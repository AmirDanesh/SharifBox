using System;

namespace SpaceApi.Domain.Models
{
    public abstract class SpaceEntityBase
    {
        public Guid Id { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}