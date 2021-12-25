using System;
using System.Collections.Generic;

namespace TeamApi.Domain.Models
{
    public class Team
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ManagerUserId { get; set; }

        public IEnumerable<TeamUser> TeamUsers { get; set; }

        public TeamDetails TeamDetails { get; set; }
    }
}