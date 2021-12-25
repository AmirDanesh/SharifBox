using System;
using System.Collections.Generic;

namespace UserApi.Domain.Models.User
{
    public class User
    {
        public Guid Id { get; set; }

        public Guid IdentityUserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string NationalCode { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;
        public string PhoneNumber { get; set; }

        public List<UserSkill> UserSkills { get; set; }
    }
}