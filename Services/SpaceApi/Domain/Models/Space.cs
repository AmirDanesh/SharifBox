using SpaceApi.Domain.Enums;
using System;

namespace SpaceApi.Domain.Models
{
    public class Space : SpaceEntityBase
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public SpaceType Type { get; set; }

        public Nullable<Guid> ParentId { get; set; }

        public Space Parent { get; set; }

        public string SvgId { get; set; }

        public RentAbleChair RentAbleChair { get; set; }
        public RentAbleRoom RentAbleRoom { get; set; }
        public RentAbleConferenceRoom RentAbleConferenceRoom { get; set; }

        public bool IsRentable()
        {
            if (Type == SpaceType.FixChair || Type == SpaceType.FlexChair || Type == SpaceType.Room || Type == SpaceType.ConferenceRoom)
                if (!IsDeleted && !IsDisabled)
                    return true;
            return false;
        }
    }
}