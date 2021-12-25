using System;
using System.Collections.Generic;

namespace UserApi.DTOs
{
    public class UserProfileDTO
    {
        public UserProfileDTO(Guid userId, Guid identityUserId, string firstName, string lastName, string address, string nationalCode, IEnumerable<Guid> skillIds)
        {
            UserId = userId;
            IdentityUserId = identityUserId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            NationalCode = nationalCode;
            SkillIds = skillIds;
        }

        public Guid UserId { get; }

        public Guid IdentityUserId { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Address { get; }

        public string NationalCode { get; }

        public IEnumerable<Guid> SkillIds { get; }
    }
}