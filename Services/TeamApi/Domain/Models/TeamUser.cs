using System;

namespace TeamApi.Domain.Models
{
    public class TeamUser
    {
        public Guid TeamId { get; set; }

        public Team Team { get; set; }

        public Guid UserId { get; set; }
    }
}