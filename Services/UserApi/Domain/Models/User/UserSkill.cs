using System;

namespace UserApi.Domain.Models.User
{
    public class UserSkill
    {
        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid SkillId { get; set; }

        public Skill Skill { get; set; }
    }
}